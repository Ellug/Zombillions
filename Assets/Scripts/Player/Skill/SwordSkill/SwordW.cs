using System.Collections;
using UnityEngine;

public class SwordW : SkillBase
{
    private float _duration = 4f;
    private float _interval = 0.03f;
    private float _attackRange = 6f;
    private float _attackAngle = 220f;
    private float _knockback = 0.2f;
    private LayerMask _layerMasks = (1 << 8) | (1 << 12);

    public SwordW()
    {        
        _skillName = "Koi no Kokyu";
        _coolTime = 10f;
        _icon = Resources.Load<Sprite>("Icons/Skill_SwordW");
    }
    
    protected override void ActivateSkill()
    {
        if (_player == null) return;
        _player.StartCoroutine(TornadoRoutine());
    }

    private IEnumerator TornadoRoutine()
    {
        float timer = 0f;

        while (timer < _duration)
        {
            timer += _interval;

            // 무작위 방향으로 회전 (220도 내)
            float randomAngle = Random.Range(-_attackAngle * 0.5f, _attackAngle * 0.5f);
            Vector3 dir = Quaternion.Euler(0, randomAngle, 0) * _player.transform.forward;

            // 시각 효과
            SlashVisualizer.DrawArc(
                _player.transform.position,
                dir,
                _attackRange,
                _attackAngle,
                0.15f,
                Color.white,
                0.25f,
                0.01f,
                0.6f
            );

            // 공격 판정
            Collider[] hits = Physics.OverlapSphere(_player.transform.position, _attackRange, _layerMasks);

            foreach (Collider hit in hits)
            {
                EnemyBase enemy = hit.GetComponent<EnemyBase>();
                if (enemy != null)
                {
                    enemy.TakeDamage(1f, _player.transform);
                    enemy.transform.position += (enemy.transform.position - _player.transform.position).normalized * _knockback;
                }

                // GoldMine 공격
                GoldMine goldMine = hit.GetComponent<GoldMine>();
                if (goldMine != null)
                    goldMine.TakeDamage(2f);
            }

            yield return new WaitForSeconds(_interval);
        }
    }
}
