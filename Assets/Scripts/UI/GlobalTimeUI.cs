using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalTimeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _gameTime;


    private GlobalTime _globalTime;

    //�� ��ȯ �� OnSceneLoaded ������ ���� ��������Ʈ
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //�� ��ȯ �� GameTime ������Ʈ�� ã�� �ִ� �޼���
    void OnSceneLoaded(Scene s, LoadSceneMode m)
    {
        if(GameObject.Find("GameTime") == null)
            return;

        GameObject gameTime = GameObject.Find("GameTime");
        _gameTime = gameTime.GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        if (_gameTime == null)
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
