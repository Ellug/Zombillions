using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshProUGUI 타입을 사용하려면 반드시 필요

/// <summary>
/// 화면에 표시할 현재 소지 자원 UI
/// </summary>
public class GoldUI : MonoBehaviour // 씬의 GameObject 에 붙여 동작하는 컴포넌트
{
    [SerializeField] private TextMeshProUGUI goldText;
    // 인스펙터에서 할당할 수 있도록 직렬화
    // 런타임에 이 참조로 텍스트 내용 변경

    private void HandleGoldChanged(int value)
    {
        if(goldText == null)
        {
            return;
        }

        goldText.text = value.ToString();
    }
    // 골드 값이 변경될 때 호출되는 콜백 함수
    // GoldManager.OnGoldChanged 이벤트에 의해 호출

    private void OnEnable()
    {
        if(GoldManager.Instance != null)
        {
            GoldManager.Instance.OnGoldChanged += HandleGoldChanged;
        }
    }
    // 컴포넌트가 활성화 될때 GoldManager 이벤트 구독
    // 이후 골드가 바뀌면 HandleGoldChanged 가 자동 호출된다

    private void OnDisable()
    {
        if(GoldManager.Instance != null)
        {
            GoldManager.Instance.OnGoldChanged -= HandleGoldChanged;
        }
    }
    // 컴포넌트 비활성화 될때 호출
    // 중복 구독 , 메모리 누수 같은 문제 대응

    void Start()
    {
        if(GameManager.Instance != null)
        {
            HandleGoldChanged(GoldManager.Instance.CurrentGold);
        }
    }
    //씬이 시작될 때 이벤트가 발생하지 않았더라도 현재 골드 값을 한번 표시
}
