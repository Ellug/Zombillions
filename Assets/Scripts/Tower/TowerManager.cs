using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;


//타워 배치(생성) / 선택 / 제거 관리
public class TowerManager : MonoBehaviour
{
    public static TowerManager Instance { get; private set; }

    [SerializeField] private LayerMask _towerLayer;
    [SerializeField] private Camera _mainCamera;
    
    private TowerSpawner _towerSpawner;
    private Tower _selectedTower;
    private bool _isMenuOpen = false;
    
    private void Awake()
    {
        if(Instance == null)
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
        //int excludeMask = LayerMask.GetMask("TowerScaner");
    }

    private void Update()
    {
        HandleSelection();
        if(_isMenuOpen == true)
        {
            BuildMenu();
        }

    }

    private void BuildMenu()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Q누름");
            _towerSpawner.BuildTower(TowerData.TowerTag.AttackTower);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("W누름");
            _towerSpawner.BuildTower(TowerData.TowerTag.DefenceTower);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E누름");
            _towerSpawner.BuildTower(TowerData.TowerTag.TriggerTower);
        }
    }

    private void HandleSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            //if (Physics.Raycast(ray, out RaycastHit hit, 50f, _towerLayer))
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Tower tower = hit.collider.GetComponent<Tower>();
                _towerSpawner = hit.collider.GetComponent<TowerSpawner>();
                
                if (tower != null)
                {
                    SelectedTower(tower);
                }
            
                else if(_towerSpawner != null)
                {
                    _isMenuOpen = true;
                }
                else
                {
                    DeselectTower();
                }
            }
        }
    }

    private void SelectedTower(Tower tower)
    {
        if (_selectedTower == tower)
        {
            return;
        }

        DeselectTower();
        _selectedTower = tower;
    }

    private void DeselectTower()
    {
        if(_selectedTower != null)
        {
            _selectedTower = null;
        }
    }
}