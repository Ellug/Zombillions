using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        // _gameLight 없으면 탐색
        if (_gameLight == null)
            _gameLight = FindAnyObjectByType<Light>();
    }

    // 씬 로드시 현재 씬의 Light로 교체
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene s, LoadSceneMode m)
    {
        _gameLight = FindAnyObjectByType<Light>();
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
        WaitForSeconds delay = new WaitForSeconds(0.1f);

        while (true)
        {
            switch (GameManager.Instance.Timer.CurrentTimeZone)
            {
                case Day.Night:
                    if (_gameLight.intensity >= _darkScale)
                        _gameLight.intensity -= _lightChangeSpeed;
                    break;
                Debug.LogError("Light 오브젝트 참조하세요");
                yield break;
            

                case Day.Noon:
                    if (_gameLight.intensity <= 1f)
                        _gameLight.intensity += _lightChangeSpeed;
                    break;
            }
            if (GameManager.Instance.Timer.CurrentTimeZone != Day.Night && _gameLight.intensity < 1f)
            {
                _gameLight.intensity += _lightChangeSpeed;
                Debug.Log("낮으로 밝기 전환");
            }

            yield return delay;
        }
    }

    //전체 밝기 조절 메서드
    public void GetLightChange(float intensity)
    {
        _gameLight.intensity = intensity;
    }
}
