using System.Collections;
using UnityEngine;

public class LIghtChanger : MonoBehaviour , ITimeObserver
{
    [SerializeField] private Light _gameLight;
    [SerializeField][Range(0 , 1)] private float _darkScale;
    [SerializeField][Range(0, 0.1f)] private float _lightChangeSpeed;

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
        StartCoroutine(SlowLightChange());
    }


    // 낮/밤이 바뀔 때 서서히 밝기가 변하는 코루틴
    private IEnumerator SlowLightChange()
    {
        while (true)
        {
            switch (GameManager.Instance.Timer.CurrentTimeZone)
            {
                case Day.Night:
                    if (_gameLight.intensity >= _darkScale)
                        _gameLight.intensity -= _lightChangeSpeed;
                    break;

                case Day.Noon:
                    if (_gameLight.intensity <= 1f)
                        _gameLight.intensity += _lightChangeSpeed;
                    break;
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
