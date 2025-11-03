using UnityEngine;

public class Gunslinger : PlayerBase
{    
    [SerializeField] private float _bulletSpeed = 160f;
    [SerializeField] private float _atkRange = 60f;
    [SerializeField] private float _knockback = 0f;
    [SerializeField] private int _pierce = 1;

    [Header("Bullet Setting")]
    [SerializeField] private Bullet.BulletSize _bulletSize = Bullet.BulletSize.Small;
    [SerializeField] private Bullet.BulletColor _bulletColor = Bullet.BulletColor.Red;

    protected override void Awake()
    {
        base.Awake();

        _skills[0] = new GunQ();
        _skills[1] = new GunW();
        _skills[2] = new GunE();
        _skills[3] = new GunR();

        foreach (var skill in _skills)
            skill.Init(this);
    }

    protected override void Attack()
    {
        Vector3 sP = transform.position + transform.forward * 0.3f;
        Vector3 dir = transform.forward;

        _bulletSpawner.Spawn(
            spawnPos: sP,
            dir: dir,
            speed: _bulletSpeed,
            dmg: _atk,
            pierce: _pierce,
            knockback: _knockback,
            range: _atkRange,
            color: _bulletColor,
            size: _bulletSize,
            AttackerTransform: this.transform
        );
    }
}
