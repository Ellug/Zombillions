using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;


//타워 배치(생성) / 선택 / 제거 관리
public class TowerManager : MonoBehaviour
{
    public static TowerManager Instance { get; private set; }

    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GameObject _towerPrefab;
    
    private Tower selectedTower;
    
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
    }

    private void Update()
    {
        HandleSelection();
    }

    private void HandleSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Tower tower = hit.collider.GetComponent<Tower>();
                if (tower != null)
                {
                    SelectedTower(tower);
                    Debug.Log($"현재 체력 : {tower.TowerCurrentHp}"); //데이터 확인용 코드
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
        if (selectedTower == tower)
        {
            return;
        }

        DeselectTower();
        selectedTower = tower;
    }

    private void DeselectTower()
    {
        if(selectedTower != null)
        {
            selectedTower = null;
        }
    }
}