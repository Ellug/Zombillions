using UnityEngine;

public class SwordMaster : PlayerBase
{
    [SerializeField] private float _attackRange = 6f;
    [SerializeField] private float _attackAngle = 160f;
    [SerializeField] private float _attackKnockback = 3f;
    private LayerMask _layerMasks = (1 << 8) | (1 << 12);

    protected override void Awake()
    {
        base.Awake();

        _skills[0] = new SwordQ();
        _skills[1] = new SwordW();
        _skills[2] = new SwordE();
        _skills[3] = new SwordR();

        foreach (var skill in _skills)
            skill.Init(this);
    }

    protected override void Attack()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _attackRange, _layerMasks);

        // 공격 시각화
        SlashVisualizer.DrawArc(
            transform.position,
            transform.forward,
            _attackRange, _attackAngle,
            0.25f,
            Color.white,
            0.01f,
            0.3f
        );

        foreach (Collider hit in hits)
        {
            Vector3 dirToTarget = (hit.transform.position - transform.position).normalized;
            float angle = Vector3.Angle(transform.forward, dirToTarget);

            if (angle <= _attackAngle * 0.5f)
            {
                EnemyBase enemy = hit.GetComponent<EnemyBase>();
                if (enemy != null)
                {
                    enemy.TakeDamage(_atk, transform);
                    enemy.transform.position += dirToTarget * _attackKnockback;
                    continue;
                }

                // GoldMine 공격
                GoldMine goldMine = hit.GetComponent<GoldMine>();
                if (goldMine != null)
                {
                    goldMine.TakeDamage(_atk);
                    continue;
                }
            }
        }
    }
}