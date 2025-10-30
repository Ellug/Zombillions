using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] private List<TowerData> _towerList;


    private bool _isBuilt = false;
    public bool IsBuilt => _isBuilt;

    public void BuildTower(TowerData.TowerTag tag)
    {
        if(_isBuilt)
        {
            Debug.Log("타워가 설치되어 있습니다.");
            return;
        }
        
        foreach (var tower in _towerList)
        {
            if(tower.towerTag == tag)
            {
                //if(tower.cost > playerGold)
                //{
                //    Debug.Log("타워 설치에 필요한 골드가 부족합니다.");
                //    return;
                //}

                GameObject towerObj = Instantiate(tower._towerPrefab, transform.position, Quaternion.identity);
                Tower towerComp = towerObj.GetComponent<Tower>();
                if (towerComp != null)
                {
                    towerComp.SetSpawner(this);
                }
                GetComponent<BoxCollider>().enabled = false;
                _isBuilt = true;
                break;
            }
        }
    }
    public void ResetBuilt()
    {
        _isBuilt = false;
        GetComponent<BoxCollider>().enabled = true;
    }
    

}
