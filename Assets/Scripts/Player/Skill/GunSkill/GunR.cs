using System.Collections;
using UnityEngine;

public class GunR : SkillBase
{
    private float _fireDuration = 8f;
    private float _fireInterval = 0.003f;
    private float _spreadAngle = 5f;
    private float _bulletSpeed = 180f;
    private float _bulletRange = 120f;
    private float _dmgMultiplier = 0.6f;
    private float _knockback = 0.1f;

    private WaitForSeconds _wait;

    public GunR()
    {
        _skillName = "Gattling Gun";
        _coolTime = 20f;
        _soundIndex = 3;
        _icon = Resources.Load<Sprite>("Icons/Skill_GunR");
    }

    public override void Init(PlayerBase player)
    {
        base.Init(player);
        _wait = new WaitForSeconds(_fireInterval);
    }

    protected override void ActivateSkill()
    {
        if (_player == null) return;

        _player.StartCoroutine(FireRoutine());
    }

    private IEnumerator FireRoutine()
    {
        float timer = 0f;

        while (timer < _fireDuration)
        {
            timer += Time.deltaTime;

            Vector3 spawnPos = _player.transform.position + _player.transform.forward * 0.3f;
            Vector3 dir = _player.transform.forward;

            // 좌우 오차 각도 적용
            float randomYaw = Random.Range(-_spreadAngle, _spreadAngle);
            Quaternion spreadRot = Quaternion.Euler(0f, randomYaw, 0f);
            Vector3 spreadDir = spreadRot * dir;

            float dmg = _player.Atk * _dmgMultiplier;

            _player._bulletSpawner.Spawn(
                spawnPos,
                spreadDir,
                _bulletSpeed,
                dmg,
                1,
                _knockback,
                _bulletRange,
                Bullet.BulletColor.Red,
                Bullet.BulletSize.Small
            );

            yield return _wait;
        }

        Debug.Log($"{_skillName} 종료");
    }
}
