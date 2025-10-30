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

    //GlobarTime�� ��/�� ��ȭ�� ��⸦ �����ϴ� ������ ����
    public void OnTimeZoneChange()
    {
        StartCoroutine(SlowTimeZoneChange());
    }


    //�̰� ���ؼ� �ʿ���
    private IEnumerator SlowTimeZoneChange()
    {
        while (true)
        {
            if (_gameLight == null)
            {
                Debug.LogError("Light ������Ʈ �����ϼ���");
                break;
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

            yield return new WaitForSeconds(0.1f);
        }
    }

    //��ü ��� ���� �޼���
    public void GetLightChange(float intensity)
    {
        _gameLight.intensity = intensity;
    }
}
