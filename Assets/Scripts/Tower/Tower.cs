using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

//타워 동작 처리
public class Tower : MonoBehaviour
{
    [SerializeField] private TowerData _towerData;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private TowerTrigger _towerTracer;

    private float _timer;
    private float _towerCurrentHp;
    public float TowerCurrentHp => _towerCurrentHp;

    private void Awake()
    {
        Init();
    }
    
    private void Init()
    {
        if (_towerData == null)
        {
            return;
        }

        _towerCurrentHp = _towerData.maxHp;

        //해당 오브젝트 태그 설정
        string tagName = _towerData.towerTag.ToString();
        gameObject.tag = tagName;

        //해당 타워 자식(타워 타입)에 공격범위 설정
        SphereCollider childCollider = GetComponentInChildren<SphereCollider>();
        if (childCollider != null)
        {
            childCollider.radius = _towerData.attackRange;
        }
    }
    private void Update()
    {
        if (_towerTracer.GetCurrentEnemy() != null)
        {
            _timer += Time.deltaTime;
            if (_timer > 16 / _towerData.attackSpeed)
            {
                _timer = 0f;
                _bullet.Spawn();
            }
        }
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
