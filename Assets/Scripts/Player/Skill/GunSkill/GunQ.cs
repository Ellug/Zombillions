using UnityEngine;

public class GunQ : SkillBase
{
    private int _bulletCount = 30;
    private float _spreadAngle = 30f;
    private float _bulletSpeed = 120f;
    private float _skillRange = 70f;
    private float _knockback = 10f;

    void Awake()
    {
        _skillName = "Shot Gun";
        _coolTime = 4f;
        _icon = Resources.Load<Sprite>("Icons/Skill_GunQ");
    }

    protected override void ActivateSkill()
    {
        if (_player == null) return;

        Vector3 baseDir = _player.transform.forward;
        Vector3 spawnPos = _player.transform.position + baseDir * 0.5f;
        float halfAngle = _spreadAngle * 0.5f;
        float dmg = _player.Atk * 0.8f;

        for (int i = 0; i < _bulletCount; i++)
        {
            float t = (float)i / (_bulletCount - 1);
            float angle = Mathf.Lerp(-halfAngle, halfAngle, t);
            Quaternion rot = Quaternion.AngleAxis(angle, Vector3.up);
            Vector3 dir = rot * baseDir;

            _player._bulletSpawner.Spawn(
                spawnPos,
                dir,
                _bulletSpeed,
                dmg,
                1,
                _knockback,
                _skillRange,
                Bullet.BulletColor.Yellow,
                Bullet.BulletSize.Small
            );
        }

        // 넉백 간단하게
        _player.transform.Translate(-_player.transform.forward * 2f, Space.World);
    }
}
