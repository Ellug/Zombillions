using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 게임 내 골드를 총괄로 관리하는 매니저
/// 싱글톤 간단 구현
/// AddGold : 획득
/// TrySpend / HasEnough : 소모 / 검증
/// OnGoldChanged : UI 등 구독 이벤트
/// </summary>

public class GoldManager : MonoBehaviour
{

    [Header("초기 골드")]
    [SerializeField] private int _startGold = 0;

    //[SerializeField] private TMPro.TextMeshProUGUI _goldText;
    //[SerializeField] private UnityEngine.UI.Text _legacyText;


    public GoldUI GoldUI {  get; private set; }
    public int CurrentGold { get; private set; }
    // 골드 값 바뀔 때마다 발생
    public event Action<int> OnGoldChanged;

    void Awake()
    {
        GoldUI = GetComponent<GoldUI>();
        CurrentGold = Mathf.Max(0, _startGold);
        OnGoldChanged = null;
    }

    // 이벤트 알림
    private void NotifyGoldChanged()
    {
        OnGoldChanged?.Invoke(CurrentGold);
    }

    /// <summary>
    /// 골드 획득 ( 금광 파괴 , 몬스터 처치 보상 등 )
    /// </summary>
    public void AddGold(int amount /*, Vector3 worldPos*/ )
    {
        CurrentGold += Mathf.Max(0, amount);
        NotifyGoldChanged();
    }

    // 코스트만큼 지불 가능한지 판단 메서드
    public void TrySpend(int cost)
    {
        if (cost <= 0 || CurrentGold < cost)
        {
            Debug.LogWarning("구매를 실패했습니다.");
            return;
        }
        CurrentGold = CurrentGold - cost;
        NotifyGoldChanged();
    }
}
