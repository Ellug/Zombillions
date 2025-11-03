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

    //public void BuildTower(TowerData.TowerTag tag, int playerGold)
    public void BuildTower(TowerData.TowerTag tag)
    {
        if(_isBuilt)
        {
            Debug.Log("Tower Built!.");
            return;
        }
        
        foreach (var tower in _towerList)
        {
            if (tower.towerTag == tag)
            {
                //if(tower.cost > playerGold)
                //{
                //    Debug.Log("You don't have enough gold to build the tower.");
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
                //Deduct the cost from the player¡¯s gold.
                break;
            }
        }
    }

    public void DestroyTower(Tower tower)
    {
        if (tower != null)
        {
            Destroy(tower.gameObject);
            tower = null;
            ResetBuilt();
        }
    }

    public void ResetBuilt()
    {
        _isBuilt = false;
        GetComponent<BoxCollider>().enabled = true;
    }
}
