using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalTimeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _gameTime;


    private GlobalTime _globalTime;

    //씬 변환 시 OnSceneLoaded 실행을 위한 델리게이트
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //씬 전환 시 GameTime 오브젝트를 찾아 넣는 메서드
    void OnSceneLoaded(Scene s, LoadSceneMode m)
    {
        if(GameObject.Find("GameTime") == null)
            return;

        GameObject gameTime = GameObject.Find("GameTime");
        _gameTime = gameTime.GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        if (_globalTime != null)
            return;

        _globalTime = GameManager.Instance.GetComponent<GlobalTime>();
    }

    void Update()
    {
        if (_gameTime != null)
        {
            int min = _globalTime.GameTime / 60;
            int sec = _globalTime.GameTime % 60;
            _gameTime.text = $"{_globalTime.CurrentTimeZone}  " + string.Format("{0:D2}:{1:D2}", min, sec);
        }
    }
}
