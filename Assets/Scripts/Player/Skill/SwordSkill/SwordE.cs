using UnityEngine;

public class SwordE : SkillBase
{
    private float _range = 90f;
    private float _damage = 1f;
    private LayerMask _enemyMask = 1 << 8;

    public SwordE()
    {        
        _skillName = "Deadly Shouting";
        _coolTime = 10f;
        _icon = Resources.Load<Sprite>("Icons/Skill_SwordE");
    }
    
    protected override void ActivateSkill()
    {
        if (_player == null) return;

        // 시각 효과
        SlashVisualizer.DrawArc(
            _player.transform.position,
            _player.transform.forward,
            _range,
            360f,
            0.3f,
            Color.red,
            0.5f,
            1.5f,
            0.5f
        );

        // 범위 내 적 탐색
        Collider[] hits = Physics.OverlapSphere(_player.transform.position, _range, _enemyMask);

        foreach (Collider hit in hits)
        {
            EnemyBase enemy = hit.GetComponent<EnemyBase>();
            if (enemy != null)
            {
                // 피해 적용
                enemy.TakeDamage(_damage, _player.transform);

                // 타겟 플레이어로 변경
                enemy._targetTransform = _player.transform;
                enemy._Chase = true;
            }
        }
    }
}
