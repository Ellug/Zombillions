using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Rm { get; private set; }

    [SerializeField] private int gold = 0;
    [SerializeField] private Text goldText;

    void Awake()
    {
        Rm = this;
        UpdateUI();
    }

    public void GainGold(int amount)
    {
        gold += amount;
        UpdateUI();
    }

    public bool TrySpend(int cost)
    {
        if(gold < cost)
        {
            return false;
        }
        gold -= cost;
        UpdateUI();
        return true;
    }

    private void UpdateUI()
    {
        if(goldText != null)
        {
            goldText.text = $"Gold : {gold}";
        }
    }
}
