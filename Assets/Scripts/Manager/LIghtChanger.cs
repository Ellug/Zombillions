using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        // _gameLight ������ Ž��
        if (_gameLight == null)
            _gameLight = FindAnyObjectByType<Light>();
    }

    // �� �ε�� ���� ���� Light�� ��ü
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

    //GlobarTime�� ��/�� ��ȭ�� ��⸦ �����ϴ� ������ ����
    public void OnTimeZoneChange()
    {
        StartCoroutine(SlowTimeZoneChange());
    }


    //�̰� ���ؼ� �ʿ���
    // new WiathForSec �κ� ������ ���� �����ȿ��� ���� ���ϴ� ����ȭ ��
    private IEnumerator SlowTimeZoneChange()
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);

        while (true)
        {
            if (_gameLight == null)
            {
                Debug.LogError("Light ������Ʈ �����ϼ���");
                yield break;
            }

            if (GameManager.Instance.Timer.CurrentTimeZone != Day.Noon && _gameLight.intensity > _gameDark)
            {
                _gameLight.intensity -= _lightChangeSpeed;
                Debug.Log("������ ��� ��ȯ");
            }
            if (GameManager.Instance.Timer.CurrentTimeZone != Day.Night && _gameLight.intensity < 1f)
            {
                _gameLight.intensity += _lightChangeSpeed;
                Debug.Log("������ ��� ��ȯ");
            }

            yield return delay;
        }
    }

    //��ü ��� ���� �޼���
    public void GetLightChange(float intensity)
    {
        _gameLight.intensity = intensity;
    }
}
