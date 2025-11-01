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
            Debug.LogError("�θ�� EnemyBase.cs �� �����ϴ�. EnemyAttack ��ũ��Ʈ�� Enemy �ڽ� ������Ʈ�� �־�� �մϴ�.");
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

        Debug.Log($"{_enemyBase.gameObject.name}��(��) {targetTag} ({target.name})���� {damage} �������� �������ϴ�!");

        // �÷��̾� ���� - �۵� Ȯ��
        if (target.CompareTag("Player"))
        {
            PlayerBase player = target.GetComponent<PlayerBase>();
            if (player != null)
            {
                // ���ʹ� �÷��̾� ���ݽ� ȿ����
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
