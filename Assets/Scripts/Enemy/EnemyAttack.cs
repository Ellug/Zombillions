using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private EnemyBase _enemyBase;
    private bool _canAttack = true;

    void Awake()
    {
        _enemyBase = GetComponentInParent<EnemyBase>();
        if (_enemyBase == null)
        {
            Debug.LogError("부모로 EnemyBase.cs 가 없습니다. EnemyAttack 스크립트는 Enemy 자식 오브젝트에 있어야 합니다.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_canAttack) return;
        string targetTag = "";

        if (other.CompareTag("Tower"))
        {
            targetTag = "Tower";
        }
        else if (other.CompareTag("Player"))
        {
            targetTag = "Player";
        }
        else if (other.CompareTag("HQ"))
        {
            targetTag = "HQ";
        }

        if (targetTag != "")
        {
            StartCoroutine(AttackRoutine(other.gameObject, targetTag));
        }
    }

    private IEnumerator AttackRoutine(GameObject target, string targetTag)
    {
        _canAttack = false;
        ApplyDamage(target, targetTag);
        yield return new WaitForSeconds(_enemyBase._AttackDelay);
        _canAttack = true;
    }

    private void ApplyDamage(GameObject target, string targetTag)
    {
        if (target == null) return;
        float damage = _enemyBase._EnemyDMG;

        Debug.Log($"{_enemyBase.gameObject.name}이(가) {targetTag} ({target.name})에게 {damage} 데미지를 입혔습니다!");

        // 플레이어 공격 - 작동 확인
        if (target.CompareTag("Player"))
        {
            PlayerBase player = target.GetComponent<PlayerBase>();
            if (player != null)
            {
                // 에너미 플레이어 공격시 효과음
                if(_enemyBase._enemyAttackSound != null)
                    GameManager.Instance.Sound.EffectSound.GetSoundEffect(_enemyBase._enemyAttackSound);

                player.TakeDamage(damage);
                return;
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
