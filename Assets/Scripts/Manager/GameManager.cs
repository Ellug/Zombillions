using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public GlobalTime Timer { get; private set; }
    public GlobalScore Score { get; private set; }
    public SceneChanger Scene { get; private set; }

    public GameState State { get; private set; }

    public GameObject SelectedPlayerPrefab { get; private set; }

    //�̱��� ����
    //1. �ı����� �ʵ��� DonDestroy
    //2. �̹� �ڽ��� �����ϸ� �ı�
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            Timer = GetComponent<GlobalTime>();
            Score = GetComponent<GlobalScore>();
            Scene = GetComponent<SceneChanger>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ���� ������ ĳ���� ���� ���� �����ؼ� ���� �Ѿ�� ���� �뵵
    public void SetSelectedCharacter(GameObject prefab)
    {
        SelectedPlayerPrefab = prefab;
    }

    public void GameStateChange()
    {
        if (State == GameState.Playing)
        {

        }
        
        if (State == GameState.GameOver)
        {
            GameOverUI gameOver = GetComponent<GameOverUI>();
        }
    }

    public enum GameState { Playing , GameOver}
}
