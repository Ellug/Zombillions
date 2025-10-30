using System.Collections;
using UnityEngine;

public class LIghtChanger : MonoBehaviour , ITimeObserver
{
    [SerializeField] private Light _gameLight;
    [SerializeField][Range(0 , 1)] private float _gameDark;
    [SerializeField][Range(0, 0.5f)] private float _lightChangeSpeed;

    private GlobalTime _globalTime;

    private void Start()
    {
        _globalTime = GameManager.Instance.Timer;
        _globalTime?.AddObserver(this);
    }

    //GlobarTime의 낮/밤 변화에 밝기를 조절하는 옵저버 패턴
    public void OnTimeZoneChange()
    {
        StartCoroutine(SlowTimeZoneChange());
    }


    //이거 재해석 필요함
    private IEnumerator SlowTimeZoneChange()
    {
        while (true)
        {
            if (_gameLight == null)
            {
                Debug.LogError("Light 오브젝트 참조하세요");
                break;
            }

            if (GameManager.Instance.Timer.CurrentTimeZone != Day.Noon && _gameLight.intensity > _gameDark)
            {
                _gameLight.intensity -= _lightChangeSpeed;
                Debug.Log("밤으로 밝기 전환");
            }
            if (GameManager.Instance.Timer.CurrentTimeZone != Day.Night && _gameLight.intensity < 1f)
            {
                _gameLight.intensity += _lightChangeSpeed;
                Debug.Log("낮으로 밝기 전환");
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    //전체 밝기 조절 메서드
    public void GetLightChange(float intensity)
    {
        _gameLight.intensity = intensity;
    }
}
