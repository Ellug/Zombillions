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


    public void OnNotify()
    {
        if (_gameLight == null)
        {
            Debug.LogError("Light ������Ʈ �����ϼ���");
            return;
        }

        
        if (GameManager.Instance.Timer.CurrentTimeZone == Day.Noon)
        {
            _gameLight.intensity = 1f;
            Debug.Log("������ ��� ��ȯ");
        }
        if (GameManager.Instance.Timer.CurrentTimeZone == Day.Night)
        {
            _gameLight.intensity = 0.5f;
            Debug.Log("������ ��� ��ȯ");
        }

    }
}
