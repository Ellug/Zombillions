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
        if (_gameLight == null)
        {
            Debug.LogError("Light ������Ʈ �����ϼ���");
            return;
        }
        StartCoroutine(SlowLightChange());
    }


    // ��/���� �ٲ� �� ������ ��Ⱑ ���ϴ� �ڷ�ƾ
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
                Debug.LogError("Light ������Ʈ �����ϼ���");
                yield break;
            

                case Day.Noon:
                    if (_gameLight.intensity <= 1f)
                        _gameLight.intensity += _lightChangeSpeed;
                    break;
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
