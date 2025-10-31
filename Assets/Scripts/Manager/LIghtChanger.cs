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

    //��ü ��� ���� �޼���
    public void GetLightChange(float intensity)
    {
        _gameLight.intensity = intensity;
    }
}
