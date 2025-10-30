
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GlobalTimeUI : MonoBehaviour
{
    [SerializeField] private Text _timeText;
    [SerializeField] private GameManager _gameManager;

    private GlobalTime _globalTime;

    private void Awake()
    {
        _globalTime = _gameManager.GetComponent<GlobalTime>();
    }

    void Start()
    {
        Debug.Log(_globalTime);
        //_timeText.text = $"{_globalTime.CurrentTimeZone}  {_globalTime.GameTime}";
        _timeText.text = "¿ﬂ ¿€µø«ÿ∫¡";
        
    }
}
