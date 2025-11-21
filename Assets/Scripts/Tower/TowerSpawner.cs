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
            Debug.Log("Tower Built!.");
            return;
        }
        
        foreach (var tower in _towerList)
        {
            if (tower.towerTag == tag)
            {
                if (tower.cost > playerGold.CurrentGold)
                {
                    Debug.Log("¼³Ä¡ ºÒ°¡ - Å¸¿ö ¼³Ä¡¿¡ ÇÊ¿äÇÑ °ñµå°¡ ºÎÁ·ÇÕ´Ï´Ù.");
                    return;
                }
                //if (Å¸¿ö »ý¼º ¹üÀ§ ¾È¿¡ ÀûÀÌ ÀÖ´Â°¡ ?)
                //{
                //    Debug.Log("¼³Ä¡ ºÒ°¡ - ÁÖÀ§¿¡ ÀûÀÌ ÀÖ½À´Ï´Ù.");
                //}

                GameObject towerObj = Instantiate(tower._towerPrefab, transform.position, Quaternion.identity);
                Tower towerComp = towerObj.GetComponent<Tower>();
                if (towerComp != null)
                {
                    towerComp.SetSpawner(this);
                }
                GetComponent<BoxCollider>().enabled = false;
                _isBuilt = true;

                //ÇÃ·¹ÀÌ¾î °ñµå cost¸¸Å­ ¼Ò¸ðÃ³¸®.
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
