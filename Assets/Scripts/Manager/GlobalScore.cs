using UnityEngine;

public class GlobalScore : MonoBehaviour
{
    public int GameScore { get; private set; }
    public int FinalTime { get; private set; }
    public int FinalWaveCount { get; private set; }
    
    //생존시간, 생존웨이브, 총합스코어 세팅
    public int GetSumScore()
    {
        FinalTime = GameManager.Instance.Timer.LifeTime;
        FinalWaveCount = GameManager.Instance.Timer.GameWaveCount;

        return GameScore += FinalTime + FinalWaveCount;
    }
}
