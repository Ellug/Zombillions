using UnityEngine;

public class GunW : SkillBase
{
    private float _bulletSpeed = 240f;
    private float _skillRange = 200f;
    private float _knockback = 10f;
    private int _penetration = 20;
    void Awake()
    {
        _skillName = "Sniper Rifle";
        _coolTime = 6f;
        _icon = Resources.Load<Sprite>("Icons/Skill_GunW");
    }
    
    protected override void ActivateSkill()
    {
        if (_player == null) return;

        Vector3 dir = _player.transform.forward;
        Vector3 spawnPos = _player.transform.position + dir * 0.5f;
        float dmg = _player.Atk * 20;      
        
        _player._bulletSpawner.Spawn(
            spawnPos,
            dir,
            _bulletSpeed,
            dmg,
            _penetration,
            _knockback,
            _skillRange,
            Bullet.BulletColor.Red,
            Bullet.BulletSize.Large
        );

        // 넉백
        _player.transform.Translate(-_player.transform.forward * 3f, Space.World);
    }
}
