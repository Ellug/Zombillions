using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public GlobalTime Timer { get; private set; }
    public GlobalScore Score { get; private set; }
    public SceneChanger Scene { get; private set; }

    public GameState State { get; private set; }

    public GameObject SelectedPlayerPrefab { get; private set; }

    //싱글톤 패턴
    //1. 파괴되지 않도록 DonDestroy
    //2. 이미 자신이 존재하면 파괴
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

    // 선택 씬에서 캐릭터 선택 정보 저장해서 갖고 넘어가기 위한 용도
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
