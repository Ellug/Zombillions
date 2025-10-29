using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private CameraController _mainCamera;
    [SerializeField] private PlayerUI _playerUI;
    [SerializeField] private SkillUI _skillUI;

    private PlayerBase _player;

    void Start()
    {
        //프리팹 로드
        var prefab = GameManager.Instance.SelectedPlayerPrefab;

        if (prefab == null)
        {
            Debug.LogError("Where is Your Prefab??");
            return;
        }

        // 생성
        _player = Instantiate(prefab, _spawnPoint.position, Quaternion.identity).GetComponent<PlayerBase>();

        // 플레이어 스폰 포인트 세팅
        _player.SetSpawnPoint(_spawnPoint);

        // 카메라 연결
        if (_mainCamera != null)
            _mainCamera.SetTarget(_player.transform);

        // UI 연결
        if (_playerUI != null)
            _playerUI.SetPlayer(_player);
        if (_skillUI != null)
            _skillUI.SetPlayer(_player);
    }
}
