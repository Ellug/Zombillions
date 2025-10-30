using UnityEngine;

public class LIghtChanger : MonoBehaviour , ITimeObserver 
{
    [SerializeField] private Light _gameLight;
    [SerializeField][Range(0 , 1)] private float _gameDark;

    private GlobalTime _globalTime;

    private void Start()
    {
        _globalTime = GameManager.Instance.Timer;
        _globalTime?.AddObserver(this);

    }

    //GlobarTime의 낮/밤 변화에 밝기를 조절하는 옵저버 패턴
    public void OnTimeZoneChange()
    {
        if (_gameLight == null)
        {
            Debug.LogError("Light 오브젝트 참조하세요");
            return;
        }

        switch (GameManager.Instance.Timer.CurrentTimeZone)
        {
            case Day.Noon:
                GetLightChange(1f);
                Debug.Log("낮으로 밝기 전환");
                break;

            case Day.Night:
                GetLightChange(_gameDark);
                Debug.Log("밤으로 밝기 전환");
                break;
        }
    }

    //전체 밝기 조절 메서드
    public void GetLightChange(float intensity)
    {
        _gameLight.intensity = intensity;
    }
}
