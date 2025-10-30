using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


//타워 배치(생성) / 선택 / 제거 관리
public class TowerManager : MonoBehaviour
{
    public static TowerManager Instance { get; private set; }

    [SerializeField] private LayerMask _towerLayer;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GameObject _spawnTowerMenuPanel;
    [SerializeField] private GameObject _towerMenuPanel;
    [SerializeField] private Button _attackTowerButton;
    [SerializeField] private Button _defenceTowerButton;
    [SerializeField] private Button _triggerTowerButton;
    [SerializeField] private Button _deleteTowerButton;


    private TowerSpawner _selectTowerSpawner;
    private Tower _selectedTower;

    private bool _isTowerSpawnerSelected = false;
    private bool _isTowerSelected = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (_mainCamera == null)
        {
            _mainCamera = Camera.main;
        }
    }

    private void Start()
    {
        int excludeMask = LayerMask.GetMask("TowerScaner");
        _towerLayer = ~excludeMask;

        InitButton();
        InitPanel();

    }

    private void Update()
    {
        HandleSelection();

        if (_isTowerSpawnerSelected == true)
        {
            SelectBuildMenu();
        }
        if (_isTowerSelected == true)
        {
            SelectTowerMenu();
        }
    }
    #region 버튼/패널 초기화
    private void InitButton()
    {
        _attackTowerButton.onClick.AddListener(Instance.BuildAttackTower);
        _defenceTowerButton.onClick.AddListener(Instance.BuildDefenceTower);
        _triggerTowerButton.onClick.AddListener(Instance.BuildTriggerTower);
        _deleteTowerButton.onClick.AddListener(Instance.DeleteTower);
    }

    private void InitPanel()
    {
        _spawnTowerMenuPanel.SetActive(false);
        _towerMenuPanel.SetActive(false);
    }
    #endregion

    #region 핸들러
    //선택 핸들러
    private void HandleSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 250f, _towerLayer))
            {
                CloseMenu();

                _selectedTower = hit.collider.GetComponent<Tower>();
                _selectTowerSpawner = hit.collider.GetComponent<TowerSpawner>();

                //Tower 선택만
                if (_selectedTower != null)
                {
                    _isTowerSelected = true;
                }
                //TowerSpawner 선택만
                else if (_selectTowerSpawner != null)
                {
                    _isTowerSpawnerSelected = true;
                }
            }
        }
    }
    #endregion

    #region 메뉴 호출
    //선택한 타워 메뉴
    private void SelectTowerMenu()
    {
        if (_selectedTower != true)
        {
            return;
        }
        _towerMenuPanel.SetActive(true);

        if (Input.GetKeyDown(KeyCode.K))
        {
            DeleteTower();
        }
    }

    //스포너 메뉴
    private void SelectBuildMenu()
    {
        if (_selectTowerSpawner != true)
        {
            return;
        }

        _spawnTowerMenuPanel.SetActive(true);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            BuildAttackTower();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            BuildDefenceTower();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            BuildTriggerTower();
        }
    }
    #endregion

    #region 스포너 각 메뉴 호출
    private void BuildAttackTower()
    {
        _selectTowerSpawner.BuildTower(TowerData.TowerTag.AttackTower);
        _isTowerSpawnerSelected = false;
        _spawnTowerMenuPanel.SetActive(false);
    }
    private void BuildDefenceTower()
    {
        _selectTowerSpawner.BuildTower(TowerData.TowerTag.DefenceTower);
        _isTowerSpawnerSelected = false;
        _spawnTowerMenuPanel.SetActive(false);
    }
    private void BuildTriggerTower()
    {
        _selectTowerSpawner.BuildTower(TowerData.TowerTag.TriggerTower);
        _isTowerSpawnerSelected = false;
        _spawnTowerMenuPanel.SetActive(false);
    }
    #endregion

    #region 타워 각 메뉴 호출
    private void DeleteTower()
    {
        //내가 현재 선택한건 타워임 = Instance는 타워값만 가지고있는 상태
        //해당 타워가 가지고있는 스포너의 DestroyTower()호출해야함. 여기서 인자값으로 타워?주면 될듯?
        //그리고 나서 다 오프시키기.
        TowerSpawner spawner = _selectedTower.GetSpawner();
        spawner.DestroyTower(_selectedTower);
        _isTowerSelected = false;
        _towerMenuPanel.SetActive(false);
    }
    #endregion

    //모든 메뉴 닫기
    private void CloseMenu()
    {
        _isTowerSpawnerSelected = false;
        _isTowerSelected = false;
        _spawnTowerMenuPanel.SetActive(false);
        _towerMenuPanel.SetActive(false);
    }

}