using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] private List<TowerData> _towerList;
    
    public void BuildTower(TowerData.TowerTag tag)
    {
        foreach (var tower in _towerList)
        {
            if(tower.towerTag == tag)
            {
                Instantiate(tower._towerPrefab, transform.position, Quaternion.identity);
            }
        }
    }
    

}
