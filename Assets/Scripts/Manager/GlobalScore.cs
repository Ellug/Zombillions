using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScore : MonoBehaviour
{
    public int GameScore { get; private set; }

    

    public int SumScore()
    {
        int finalTime = GameManager.Instance.Timer.GameTime;
        int finalWave = GameManager.Instance.Timer.GameWave;

        return GameScore += finalTime + finalWave;
    }
}
