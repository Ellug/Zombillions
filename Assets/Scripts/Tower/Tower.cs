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
    public TowerData TowerData => _towerData;

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
            childCollider.radius = _towerData.attackRange /childCollider.transform.lossyScale.z;
        }

        _bulletSpawner = FindObjectOfType<BulletSpawner>();
        if (_bulletSpawner == null)
        {
            Debug.LogError("BulletSpawner��ã��.");
        }

        //타워 트리거에 데이터 전달하기..
        if(_towerTracer != null)
        {
            _towerTracer.SetTowerData(_towerData);
        }
    }
    private void LateUpdate()
    {
        if (_towerTracer.GetCurrentEnemy() != null)
        {
            Attack();
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
                        speed: _towerData.bulletSpeed,
                        dmg: _towerData.attackPower,
                        pierce: _towerData.pierce,
                        knockback: _towerData.knockback,
                        range: _towerData.attackRange + 5f,
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
            // Destroy(gameObject);
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"[Tower] {name} 파괴됨. tag={_towerData.towerTag}");

        Destroy(gameObject);

        // if HQ Tower destroy = game over
        if (_towerData.towerTag == TowerData.TowerTag.HQTower)
        {
            Debug.Log("HQ 파괴 됐음");
            GameManager.Instance.GetGameStateChange(GameManager.GameState.GameOver);
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

    // Die 동작 테스트 코드 - 게임오버 확인 완료시 제거
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            Die();
    }
}
