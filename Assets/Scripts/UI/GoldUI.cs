using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// ȭ�鿡 ǥ���� ���� ���� �ڿ�
/// </summary>
public class GoldUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldText;

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
