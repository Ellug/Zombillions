using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor.UI;

/// <summary>
/// ���� �� ��带 �Ѱ��� �����ϴ� �Ŵ���
/// �̱��� ���� ����
/// AddGold : ȹ��
/// TrySpend / HasEnough : �Ҹ� / ����
/// OnGoldChanged : UI �� ���� �̺�Ʈ
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
            Debug.LogWarning("�ߺ� �ν��Ͻ� �����Ǿ� ���� ��ü ����");
            Destroy(this.gameObject);
            return;
        }

        _instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    [Header("�ʱ� ���")]
    [SerializeField] private int _startGold = 0;

    public int CurrentGold { get; private set; }

    // ��� �� �ٲ� ������ �߻�
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
        // �̺�Ʈ �˸�
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
    /// ��� ȹ�� ( �ݱ� �ı� , ���� óġ ���� �� )
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
