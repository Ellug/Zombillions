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

    //GlobarTime�� ��/�� ��ȭ�� ��⸦ �����ϴ� ������ ����
    public void OnTimeZoneChange()
    {
        if (_gameLight == null)
        {
            Debug.LogError("Light ������Ʈ �����ϼ���");
            return;
        }

        switch (GameManager.Instance.Timer.CurrentTimeZone)
        {
            case Day.Noon:
                GetLightChange(1f);
                Debug.Log("������ ��� ��ȯ");
                break;

            case Day.Night:
                GetLightChange(_gameDark);
                Debug.Log("������ ��� ��ȯ");
                break;
        }
    }

    //��ü ��� ���� �޼���
    public void GetLightChange(float intensity)
    {
        _gameLight.intensity = intensity;
    }
}
