using UnityEngine;
using TMPro;

public class GlobalScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private TextMeshProUGUI _playTime;
    [SerializeField] private TextMeshProUGUI _waveCount;

    private int _finalScore;
    private int _finalPlayTime;
    private int _finalWaveCount;

    void OnEnable()
    {
        if (_score == null || _playTime == null || _waveCount == null)
        {
            Debug.LogError("텍스트UI를 GlobalScoreUI에 추가하세요");
            return;
        }

        //게임매니저 통해서 스코어값을 넘겨받고
        _finalScore = GameManager.Instance.Score.GetSumScore();
        _finalPlayTime = GameManager.Instance.Score.FinalTime;
        _finalWaveCount = GameManager.Instance.Score.FinalWaveCount;

        //UI로 스코어값 출력
        int min = _finalPlayTime / 60;
        int sec = _finalPlayTime % 60;

        _playTime.text = string.Format("{0:D2}:{1:D2}", min, sec); 
        _waveCount.text = _finalWaveCount.ToString();
        _score.text = _finalScore.ToString();
    }
}