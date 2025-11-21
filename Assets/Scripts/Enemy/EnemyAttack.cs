using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] float _attackRange = 3f;

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

    private void Start()
    {
        StartCoroutine(CheckAttackRangeRoutine());
    }

    private IEnumerator CheckAttackRangeRoutine()
    {
        float randomWait = Random.Range(0.7f, 1.5f);
        WaitForSeconds wait = new WaitForSeconds(randomWait);

        while (true)
        {
            if (_canAttack)
            {
                Collider[] hits = Physics.OverlapSphere(transform.position, _attackRange);
                foreach (Collider hit in hits)
                {
                    if (hit.CompareTag("Player") || hit.CompareTag("Tower") || hit.CompareTag("HQ"))
                    {
                        StartCoroutine(AttackRoutine(hit.gameObject, hit.tag));
                        break;
                    }
                }
            }
            yield return wait;
        }
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (!_canAttack) return;
    //     string targetTag = "";

    //     if (other.CompareTag("Tower"))
    //     {
    //         targetTag = "Tower";
    //     }
    //     else if (other.CompareTag("Player"))
    //     {
    //         targetTag = "Player";
    //     }
    //     else if (other.CompareTag("HQ"))
    //     {
    //         targetTag = "HQ";
    //     }

    //     if (targetTag != "")
    //     {
    //         StartCoroutine(AttackRoutine(other.gameObject, targetTag));
    //     }
    // }

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

        // 공격
        if (target.CompareTag("Player"))
        {
            PlayerBase player = target.GetComponent<PlayerBase>();
            if (player != null)
            {
                // 에너미 플레이어 공격시 효과음
                if (_enemyBase._enemyAttackSound != null)
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
        
        if (target.CompareTag("HQ"))
        {
            Tower hq = target.GetComponent<Tower>();
            if (hq != null)
            {
                hq.TakeDamage(damage);
                return;
            }
        }
    }
}
