using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


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

    //Player�� gold �� �޾ƿ���(int��).
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
    #region ��ư/�г� �ʱ�ȭ
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

    #region �ڵ鷯
    //���� �ڵ鷯
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
                //Tower ���ø�
                if (_selectedTower != null && _selectedTower.TowerData.towerTag != TowerData.TowerTag.HQTower)
                {
                    _isTowerSelected = true;
                }
                //TowerSpawner ���ø�
                else if (_selectTowerSpawner != null)
                {
                    _isTowerSpawnerSelected = true;
                }
            }
        }
    }
    #endregion

    #region �޴� ȣ��
    //������ Ÿ�� �޴�
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

    //������ �޴�
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

    #region ������ �� �޴� ȣ��
    //�����ϰ��ִ� ��� ó�� �Ϸ� �Ǿ����� ���ڰ��� int���� playerGold �߰��ϱ�.
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

    #region Ÿ�� �� �޴� ȣ��
    private void DeleteTower()
    {
        TowerSpawner spawner = _selectedTower.GetSpawner();
        spawner.DestroyTower(_selectedTower);
        _isTowerSelected = false;
        _towerMenuPanel.SetActive(false);
    }
    #endregion

    //��� �޴� �ݱ�
    private void CloseMenu()
    {
        _isTowerSpawnerSelected = false;
        _isTowerSelected = false;
        _spawnTowerMenuPanel.SetActive(false);
        _towerMenuPanel.SetActive(false);
    }

}