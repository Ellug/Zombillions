using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor.UI;

/// <summary>
/// 게임 내 골드를 총괄로 관리하는 매니저
/// 싱글톤 간단 구현
/// AddGold : 획득
/// TrySpend / HasEnough : 소모 / 검증
/// OnGoldChanged : UI 등 구독 이벤트
/// </summary>

public class GoldManager : MonoBehaviour
{
    // Singleton
    private static GoldManager _instance;
    public static GoldManager Instance
    {
        get { return _instance; }
    }

    public static bool HasInstance
    {
        get
        {
            if (_instance != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Debug.LogWarning("중복 인스턴스 감지되어 이전 개체 유지");
            Destroy(this.gameObject);
            return;
        }

        _instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    [Header("초기 골드")]
    [SerializeField] private int _startGold = 0;

    public int CurrentGold { get; private set; }

    // 골드 값 바뀔 때마다 발생
    public event Action<int> OnGoldChanged;

    [SerializeField] private TMPro.TextMeshProUGUI _goldText;
    //[SerializeField] private UnityEngine.UI.Text _legacyText;

    [Header("Floating Text")]
    [SerializeField] private FloatingText _floatingTextPrefab;
    [SerializeField] private Canvas _uiCanvas;


    void Start()
    {
        CurrentGold = Mathf.Max(0, _startGold);
        NotifyGoldChanged();
    }

    private void NotifyGoldChanged()
    {
        // 이벤트 알림
        OnGoldChanged?.Invoke(CurrentGold);

        if(_goldText != null)
        {
            _goldText.text = $"Gold : {CurrentGold:n0}";
        }

        //if(_legacyText != null)
        //{
        //    _legacyText.text = $"Gold : {CurrentGold:n0}";
        //}
    }

    /// <summary>
    /// 골드 획득 ( 금광 파괴 , 몬스터 처치 보상 등 )
    /// </summary>
    public void AddGold(int amount, Vector3 worldPos)
    {
        if (amount <= 0)
        {
            return;
        }

        CurrentGold += amount;

        if(_floatingTextPrefab != null && _uiCanvas != null)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

            FloatingText newText = Instantiate(_floatingTextPrefab, _uiCanvas.transform);
            newText.Setup($"+{amount}", Color.yellow, screenPos);
        }

        NotifyGoldChanged();
    }

    public bool HasEnough(int cost)
    {
        if(cost < 0)
        {
            return true;
        }

        return CurrentGold >= cost;
    }

    public bool TrySpend(int cost)
    {
        if(cost <= 0)
        {
            return true;
        }

        if(CurrentGold < cost)
        {
            return false;
        }

        CurrentGold = CurrentGold - cost;
        NotifyGoldChanged();
        return true;
    }
}
