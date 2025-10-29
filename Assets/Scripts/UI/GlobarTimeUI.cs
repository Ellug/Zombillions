using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GlobarTimeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _gameTime;


    private GlobalTime _globalTime;

    void Awake()
    {
        _globalTime = GetComponent<GlobalTime>();
        if (_gameTime == null)
            Debug.LogError("텍스트UI를 GlobarTimeUI에 넣어주세요");
    }

    void Update()
    {
        int min = _globalTime.GameTime / 60;
        int sec = _globalTime.GameTime % 60;
        _gameTime.text = $"{_globalTime.CurrentTimeZone}  " + string.Format("{0:D2}:{1:D2}", min , sec);
    }

}
