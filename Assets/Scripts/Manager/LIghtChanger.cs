using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LIghtChanger : MonoBehaviour , ITimeObserver
{
    [SerializeField] private Light _gameLight;
    [SerializeField][Range(0 , 1)] private float _darkScale;
    [SerializeField][Range(0, 0.1f)] private float _lightChangeSpeed;

    private GlobalTime _globalTime;

    void Start()
    {
        _globalTime = GameManager.Instance.Timer;
        _globalTime?.AddObserver(this);

        // _gameLight 없으면 탐색
        if (_gameLight == null)
            _gameLight = FindAnyObjectByType<Light>();
    }

    // 씬 로드시 현재 씬의 Light로 교체
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene s, LoadSceneMode m)
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


    //이거 재해석 필요함
    // new WiathForSec 부분 변수로 빼면 루프안에서 생성 안하니 최적화 됨
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

                case Day.Noon:
                    if (_gameLight.intensity <= 1f)
                        _gameLight.intensity += _lightChangeSpeed;
                    break;
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
