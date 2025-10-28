using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

//Ÿ�� ���� ó��
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

        //�ش� ������Ʈ �±� ����
        string tagName = _towerData.towerTag.ToString();
        gameObject.tag = tagName;

        //�ش� Ÿ�� �ڽ�(Ÿ�� Ÿ��)�� ���ݹ��� ����
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

        Debug.Log($"Ÿ�� ü���� {_towerCurrentHp}�� �Ǿ����ϴ�."); //������ Ȯ�ο� �ڵ�

        if (_towerCurrentHp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
