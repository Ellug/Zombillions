using System.Collections;
using UnityEngine;

public class SwordR : SkillBase
{
    private float _duration = 2.2f;
    private float _interval = 0.02f;
    private float _range = 60f;
    private float _attackRange = 12f;
    private float _attackAngle = 200f;
    private float _knockback = 5f;
    private LayerMask _enemyMask = 1 << 8;

    public SwordR()
    {        
        _skillName = "Ultimate Hekireki Itsen";
        _coolTime = 20f;
        _icon = Resources.Load<Sprite>("Icons/Skill_SwordR");
    }

    protected override void ActivateSkill()
    {
        if (_player == null) return;
        _player.StartCoroutine(DashStormRoutine());
    }

    private IEnumerator DashStormRoutine()
    {
        float timer = 0f;
        Vector3 originPos = _player.transform.position;

        while (timer < _duration)
        {
            timer += _interval;

            // 시전 반경 내의 적만 탐색
            Collider[] hits = Physics.OverlapSphere(originPos, _range, _enemyMask);

            if (hits.Length > 0)
            {
                // 무작위 적 선택
                Transform target = hits[Random.Range(0, hits.Length)].transform;

                // 타겟이 반경 바깥이면 무시
                float distFromOrigin = Vector3.Distance(originPos, target.position);
                if (distFromOrigin > _range) continue;

                Vector3 dir = (target.position - _player.transform.position).normalized;

                // 플레이어 이동 위치 계산 (적 앞 2m)
                Vector3 nextPos = target.position - dir * 2f;

                // 이동 후, 원 기준 거리 초과 시 클램프 처리
                if (Vector3.Distance(originPos, nextPos) > _range)
                {
                    Vector3 limitedDir = (nextPos - originPos).normalized;
                    nextPos = originPos + limitedDir * _range * 0.95f;
                }

                // 위치 이동
                _player.transform.position = nextPos;

                // 공격 판정
                Collider[] enemies = Physics.OverlapSphere(_player.transform.position, _attackRange, _enemyMask);

                foreach (Collider enemyCol in enemies)
                {
                    Vector3 dirToTarget = (enemyCol.transform.position - _player.transform.position).normalized;
                    float angle = Vector3.Angle(dir, dirToTarget);

                    if (angle <= _attackAngle * 0.5f)
                    {
                        EnemyBase enemy = enemyCol.GetComponent<EnemyBase>();
                        if (enemy != null)
                        {
                            enemy.TakeDamage(_player.Atk * 3f, _player.transform);
                            enemy.transform.position += dirToTarget * _knockback;
                        }
                    }
                }

                // 시각 효과
                SlashVisualizer.DrawArc(
                    _player.transform.position,
                    dir,
                    _attackRange,
                    _attackAngle,
                    0.05f,
                    Color.red,
                    0.02f,
                    0.5f,
                    0.5f
                );
            }

            yield return new WaitForSeconds(_interval);
        }
    }
}