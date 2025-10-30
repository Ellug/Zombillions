using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// 화면에 표시할 현재 소지 자원
/// </summary>
public class GoldUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldText;

    // 골드 값이 변경될 때 호출되는 콜백 함수
    // GoldManager.OnGoldChanged 이벤트에 의해 호출
    private void HandleGoldChanged(int value)
    {
        if(goldText == null)
        {
            return;
        }

        goldText.text = value.ToString();
    }
    
    private void OnEnable()
    {
        if(GoldManager.Instance != null)
        {
            GoldManager.Instance.OnGoldChanged += HandleGoldChanged;
        }
    }

    private void OnDisable()
    {
        if(GoldManager.Instance != null)
        {
            GoldManager.Instance.OnGoldChanged -= HandleGoldChanged;
        }
    }

    void Start()
    {
        if(GameManager.Instance != null)
        {
            HandleGoldChanged(GoldManager.Instance.CurrentGold);
        }
    }
}
