using UnityEngine;

public class GlobalScore : MonoBehaviour
{
    public int GameScore { get; private set; }
    public int FinalTime { get; private set; }
    public int FinalWaveCount { get; private set; }
    
    //�����ð�, �������̺�, ���ս��ھ� ����
    public int GetSumScore()
    {
        FinalTime = GameManager.Instance.Timer.LifeTime;
        FinalWaveCount = GameManager.Instance.Timer.GameWaveCount;

        return GameScore += FinalTime + FinalWaveCount;
    }
}
