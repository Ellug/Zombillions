using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BommerAttack : MonoBehaviour
{
    private EnemyBase _enemyBase;
    [Header("Explosion Settings")]
    [SerializeField] public float  _ExplosionRange= 5f; 
    public LayerMask TargetLayer;      

    void Awake()
    {
        _enemyBase = GetComponentInParent<EnemyBase>();
        if (_enemyBase == null)
        {
            Debug.LogError("부모로 EnemyBase.cs 가 없습니다. BommerAttack 스크립트는 Enemy 자식 오브젝트에 있어야 합니다.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tower") || other.CompareTag("Player") || other.CompareTag("HQ"))
        {
            Explode();
        }
    }

    private void Explode()
    {
        if (_enemyBase == null) return;
        float damage = _enemyBase._EnemyDMG;

        Collider[] hitColliders = Physics.OverlapSphere(
            transform.position,
            _ExplosionRange,
            TargetLayer
        );

        foreach (var hitCollider in hitColliders)
        {
            ApplyDamage(hitCollider.gameObject, damage);
        }
        _enemyBase.Die();
    }

    private void ApplyDamage(GameObject target, float damage)
    {
        string TargetTag = target.tag;
        if (TargetTag == "Player")
        {
            PlayerBase player = target.GetComponent<PlayerBase>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }

        if (target.CompareTag("Tower"))
        {
            Tower tower = target.GetComponent<Tower>();
            if (tower != null)
            {
                tower.TakeDamage(damage);
                return;
            }
        }
    }
}
