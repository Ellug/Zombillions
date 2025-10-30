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
    [SerializeField] private Button _attackTowerButton;
    [SerializeField] private Button _defenceTowerButton;
    [SerializeField] private Button _triggerTowerButton;
    

    private TowerSpawner _selectTowerSpawner;
    private Tower _selectedTower;

    private bool _isMenuOpen = false;
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
        _attackTowerButton.onClick.AddListener(Instance.BuildAttackTower);
        _defenceTowerButton.onClick.AddListener(Instance.BuildDefenceTower);
        _triggerTowerButton.onClick.AddListener(Instance.BuildTriggerTower);
        _spawnTowerMenuPanel.SetActive(false);
    }

    private void Update()
    {
        HandleSelection();

        if (_isMenuOpen == true)
        {
            BuildMenu();
        }
        if(_isTowerSelected == true)
        {
            SelectedTowerMenu();
        }
    }


    //���� �ڵ鷯
    private void HandleSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 50f, _towerLayer))
            {
                _selectedTower = hit.collider.GetComponent<Tower>();
                _selectTowerSpawner = hit.collider.GetComponent<TowerSpawner>();

                //Tower ���ø�
                if (_selectedTower != null)
                {
                    _isTowerSelected = true;
                }
                //TowerSpawner ���ø�
                else if (_selectTowerSpawner != null)
                {
                    _isMenuOpen = true;
                }
                else
                {
                    _isMenuOpen = false;
                    _isTowerSelected = false;
                    _spawnTowerMenuPanel.SetActive(false);
                }
            }
        }
    }
 
    //������ Ÿ�� �޴�
    private void SelectedTowerMenu()
    {
        if (_selectedTower != true)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            DestroyTower(_selectedTower);
            _isTowerSelected = false;
        }
    }

    //������ �޴�
    private void BuildMenu()
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
    
    //Ÿ�� ����
    private void DestroyTower(Tower tower)
    {
        TowerSpawner spawner = tower.GetSpawner();
        if (spawner != null)
        {
            spawner.ResetBuilt();
            Destroy(tower.gameObject);
        }
    }


    private void BuildAttackTower()
    {
        _selectTowerSpawner.BuildTower(TowerData.TowerTag.AttackTower);
        _isMenuOpen = false;
        _spawnTowerMenuPanel.SetActive(false);
    }
    private void BuildDefenceTower()
    {
        _selectTowerSpawner.BuildTower(TowerData.TowerTag.DefenceTower);
        _isMenuOpen = false;
        _spawnTowerMenuPanel.SetActive(false);
    }
    private void BuildTriggerTower()
    {
        _selectTowerSpawner.BuildTower(TowerData.TowerTag.TriggerTower);
        _isMenuOpen = false;
        _spawnTowerMenuPanel.SetActive(false);
    }

    
}