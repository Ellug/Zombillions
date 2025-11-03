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
        GoldManager playerGold = GameManager.Instance.Gold;
        if(_isBuilt)
        {
            Debug.Log("타워가 설치되어 있습니다.");
            return;
        }
        
        foreach (var tower in _towerList)
        {
            if (tower.towerTag == tag)
            {
                if (tower.cost > playerGold.CurrentGold)
                {
                    Debug.Log("설치 불가 - 타워 설치에 필요한 골드가 부족합니다.");
                    return;
                }
                //if (타워 생성 범위 안에 적이 있는가 ?)
                //{
                //    Debug.Log("설치 불가 - 주위에 적이 있습니다.");
                //}

                GameObject towerObj = Instantiate(tower._towerPrefab, transform.position, Quaternion.identity);
                Tower towerComp = towerObj.GetComponent<Tower>();
                if (towerComp != null)
                {
                    towerComp.SetSpawner(this);
                }
                GetComponent<BoxCollider>().enabled = false;
                _isBuilt = true;

                //플레이어 골드 cost만큼 소모처리.
                playerGold.TrySpend(tower.cost);
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
