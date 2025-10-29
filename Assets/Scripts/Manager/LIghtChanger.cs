using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LIghtChanger : MonoBehaviour , ITimeObserver 
{
    [SerializeField] private Light _gameLight;
    [SerializeField][Range(0 , 1)] private float _gameDark;

    private GlobalTime _globalTime;

    private void Awake()
    {
        _globalTime?.AddObserver(this);
    }


    public void OnNotify()
    {
        if (GameManager.Instance.Timer.CurrentTimeZone == Day.Noon)
        {
            _gameLight.intensity = 1f;
            Debug.Log("≥∑¿∏∑Œ π‡±‚ ¿¸»Ø");
        }
        if (GameManager.Instance.Timer.CurrentTimeZone == Day.Night)
        {
            _gameLight.intensity = 0.5f;
            Debug.Log("π„¿∏∑Œ π‡±‚ ¿¸»Ø");
        }

    }
}
