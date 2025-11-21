using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


//Ÿ�� ��ġ(����) / ���� / ���� ����
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
    [SerializeField] private TowerSpawner _SetHQTowerSpawner;
    [SerializeField] private TextMeshProUGUI _attackCostText;
    [SerializeField] private TextMeshProUGUI _defenceCostText;
    [SerializeField] private TextMeshProUGUI _triggerCostText;
    [SerializeField] private TowerData _AttackTowerData;
    [SerializeField] private TowerData _DefenceTowerData;
    [SerializeField] private TowerData _TriggerTowerData;

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

        BuildHQTower();
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
    #region button UI Setting
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

    #region Handlers
    //Handlers
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
                //Tower Select
                if (_selectedTower != null && _selectedTower.TowerData.towerTag != TowerData.TowerTag.HQTower)
                {
                    _isTowerSelected = true;
                }
                //TowerSpawner Select
                else if (_selectTowerSpawner != null)
                {
                    _isTowerSpawnerSelected = true;
                }
            }
        }
    }
    #endregion

    #region Menus
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

    private void SelectBuildMenu()
    {
        if (_selectTowerSpawner != true)
        {
            return;
        }

        _spawnTowerMenuPanel.SetActive(true);

        _attackCostText.text = $"Cost: {_AttackTowerData.cost}";
        _defenceCostText.text = $"Cost: {_DefenceTowerData.cost}";
        _triggerCostText.text = $"Cost: {_TriggerTowerData.cost}";

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
    private void CloseMenu()
    {
        _isTowerSpawnerSelected = false;
        _isTowerSelected = false;
        _spawnTowerMenuPanel.SetActive(false);
        _towerMenuPanel.SetActive(false);
    }
    #endregion

    #region call Tower Build
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
    private void BuildHQTower()
    {
        _SetHQTowerSpawner.BuildTower(TowerData.TowerTag.HQTower);
    }
    #endregion

    private void DeleteTower()
    {
        TowerSpawner spawner = _selectedTower.GetSpawner();
        spawner.DestroyTower(_selectedTower);
        _isTowerSelected = false;
        _towerMenuPanel.SetActive(false);
    }
}