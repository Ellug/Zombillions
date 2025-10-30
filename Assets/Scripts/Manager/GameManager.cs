using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public GlobalTime Timer { get; private set; }
    public GlobalScore Score { get; private set; }
    public SceneChanger Scene { get; private set; }

    public LIghtChanger Light {  get; private set; }

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
            Light = GetComponent<LIghtChanger>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            GetGameStateChange(GameState.GameOver);
        }
    }

    // ���� ������ ĳ���� ���� ���� �����ؼ� ���� �Ѿ�� ���� �뵵
    public void SetSelectedCharacter(GameObject prefab)
    {
        SelectedPlayerPrefab = prefab;
    }


    //���ӽ������������� ���� ���� �޼���
    public void GetGameStateChange(GameState state)
    {
        State = state;

        switch (state)
        {
            case GameState.Playing:
                break;

            case GameState.GameOver:
                GameOverUI gameOver = GetComponent<GameOverUI>();
                gameOver.GetGameOver();
                break;
        }
    }

    public enum GameState { Playing , GameOver}
}
