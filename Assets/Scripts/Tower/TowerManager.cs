using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;


//Ÿ�� ��ġ(����) / ���� / ���� ����
public class TowerManager : MonoBehaviour
{
    public static TowerManager Instance { get; private set; }

    [SerializeField] private LayerMask _towerLayer;
    [SerializeField] private Camera _mainCamera;

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
        }
    }

    //������ �޴�
    private void BuildMenu()
    {
        if (_selectTowerSpawner != true)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Q����");
            _selectTowerSpawner.BuildTower(TowerData.TowerTag.AttackTower);
            _isMenuOpen = false;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("W����");
            _selectTowerSpawner.BuildTower(TowerData.TowerTag.DefenceTower);
            _isMenuOpen = false;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E����");
            _selectTowerSpawner.BuildTower(TowerData.TowerTag.TriggerTower);
            _isMenuOpen = false;
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

}