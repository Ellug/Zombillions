using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//타워 동작 처리
public class Tower : MonoBehaviour
{
    [SerializeField] private TowerData towerData; 

    private float _towerCurrentHp;
    public float TowerCurrentHp => _towerCurrentHp;

    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        if(towerData == null)
        {
            return;
        }

        //해당 오브젝트 태그 설정
        string tagName = towerData.towerTag.ToString();
        gameObject.tag = tagName;

        _towerCurrentHp = towerData.maxHp;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        TakeDamage(2f); //데이터 확인용 코드
    }

    public void TakeDamage(float damage)
    {
        _towerCurrentHp -= damage;

        Debug.Log($"타워 체력이 {_towerCurrentHp}이 되었습니다."); //데이터 확인용 코드

        if (_towerCurrentHp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
