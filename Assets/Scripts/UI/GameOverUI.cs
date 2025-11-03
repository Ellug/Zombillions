using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField]  private GameObject _gameOverPrefab; // 이미지 -> 게임 오브젝트

    void Start()
    {
        if (_gameOverPrefab == null)
        {
            Debug.LogError("GameOver 프리팹 필요");
            return;
        }
    }

    // Awake가 아니라 GetGameOver 작동 시점에서 Canvas 탐색
    public void GetGameOver()
    {
        GameManager.Instance.Light.GetLightChange(0f);

        GameObject canvasObj = GameObject.Find("GameOverCanvas");
        if (canvasObj == null)
        {
            Debug.LogError("[GameOverUI] GameOverCanvas를 찾을 수 없습니다.");
            return;
        }

        Transform canvasTransform = canvasObj.transform;

        Instantiate(_gameOverPrefab, canvasTransform.position, Quaternion.identity, canvasTransform);
    }
}