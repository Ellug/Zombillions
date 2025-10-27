using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class GoldSink : MonoBehaviour
{
    [SerializeField] private int gold;
    [SerializeField] private Text uiText;

    public void Gain(int amount)
    {
        if(amount <= 0)
        {
            return;
        }

        gold += amount;

        if(uiText != null)
        {
            uiText.text = $"Gold : {gold}";
        }
    }
}
