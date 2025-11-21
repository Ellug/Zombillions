using UnityEngine;

public class SwordQ : SkillBase
{
    private float _dashDistance = 12f;
    private float _attackRange = 5f;
    private float _attackAngle = 180f;
    private float _knockback = 5f;
    private LayerMask _layerMasks = (1 << 8) | (1 << 12);

    public SwordQ()
    {
        _skillName = "Hekireki Itsen";
        _coolTime = 3f;
        _icon = Resources.Load<Sprite>("Icons/Skill_SwordQ");
    }

    protected override void ActivateSkill()
    {
        if (_player == null) return;

        // 즉시 전진
        Vector3 dashPos = _player.transform.position + _player.transform.forward * _dashDistance;
        _player.transform.position = dashPos;

        // 공격 판정
        Collider[] hits = Physics.OverlapSphere(_player.transform.position, _attackRange, _layerMasks);

        // 시각 효과
        SlashVisualizer.DrawArc(
            _player.transform.position,
            _player.transform.forward,
            _attackRange, _attackAngle,
            0.25f,
            Color.yellow,
            0.01f,
            0.9f
        );

        foreach (Collider hit in hits)
        {
            Vector3 dirToTarget = (hit.transform.position - _player.transform.position).normalized;
            float angle = Vector3.Angle(_player.transform.forward, dirToTarget);

            if (angle <= _attackAngle * 0.5f)
            {
                EnemyBase enemy = hit.GetComponent<EnemyBase>();
                if (enemy != null)
                {
                    enemy.TakeDamage(_player.Atk * 3f, _player.transform);
                    enemy.transform.position += dirToTarget * _knockback;
                }

                // GoldMine 공격
                GoldMine goldMine = hit.GetComponent<GoldMine>();
                if (goldMine != null)
                    goldMine.TakeDamage(10f);
            }
        }
    }
}
