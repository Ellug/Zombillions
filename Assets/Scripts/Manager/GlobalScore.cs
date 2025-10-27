using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScore : MonoBehaviour
{
    //private int score;

    public int Score { get; set; }
    

    public void SumScore()
    {

        if (GameManager.Instance.GetComponent<GlobalTime>() != null)
        {
            GlobalTime wave = GameManager.Instance.GetComponent<GlobalTime>();
        }
        
    }

    private void Start()
    {
        SumScore();
    }
}
