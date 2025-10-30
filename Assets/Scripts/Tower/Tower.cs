using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using static Bullet;

//Ÿ�� ���� ó��
public class Tower : MonoBehaviour
{
    [SerializeField] private TowerData _towerData;
    [SerializeField] private TowerTrigger _towerTracer;
    [SerializeField] private Bullet.BulletColor _bulletColor;
    [SerializeField] private Bullet.BulletSize _bulletSize;
    [SerializeField] private Transform _bulletSpawnPoint;

    private BulletSpawner _bulletSpawner;
    private TowerSpawner _mySpawner;
    private float _timer;
    private float _towerMaxHp;
    private float _towerCurrentHp;
    public float TowerMaxHp => _towerMaxHp;
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

        _towerMaxHp = _towerData.maxHp;
        _towerCurrentHp = _towerData.maxHp;

        //�ش� Ÿ�� �ڽ�(Ÿ�� Ÿ��)�� ���ݹ��� ����
        SphereCollider childCollider = GetComponentInChildren<SphereCollider>();
        if (childCollider != null)
        {
            childCollider.radius = _towerData.attackRange;
        }

        _bulletSpawner = FindObjectOfType<BulletSpawner>();
        if (_bulletSpawner == null)
        {
            Debug.LogError("BulletSpawner��ã��.");
        }
    }
    private void Update()
    {
        if (_towerTracer.GetCurrentEnemy() != null)
        {
            Attack();
        }
        ////////////////////////////////////////////////////////////�׽�Ʈ��
        if (Input.GetKeyDown(KeyCode.X))
        {
            TakeDamage(5);
        }
    }

    private void Attack()
    {
        Vector3 spawnPoint = _bulletSpawnPoint.position + _bulletSpawnPoint.forward;
        Vector3 targetPoint = _towerTracer.GetCurrentEnemy().transform.position;
        Vector3 dir = targetPoint - spawnPoint;

        if (_towerTracer.GetCurrentEnemy() != null)
        {
            _timer += Time.deltaTime;
            if (_timer > 16 / _towerData.attackSpeed)
            {
                _timer = 0f;
                _bulletSpawner.Spawn(
                    spawnPos: spawnPoint,
                    dir: dir,
                    speed: 50,
                    dmg: _towerData.attackPower,
                    pierce: 0,
                    knockback: 0,
                    range: _towerData.attackRange,
                    color: _bulletColor,
                    size: _bulletSize
                );
            }
        }
    }

    public void TakeDamage(float damage)
    {
        _towerCurrentHp -= damage;

        if (_towerCurrentHp <= 0)
        {
            _mySpawner?.ResetBuilt();
            Destroy(gameObject);
        }
    }

    //������ �����Ű��
    public void SetSpawner(TowerSpawner spawner)
    {
        _mySpawner = spawner;
    }
    public TowerSpawner GetSpawner()
    {
        return _mySpawner;
    }
}
