using System.Collections.Generic;
using UnityEngine;


public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private int _bulletPoolSize = 50;

    private Queue<Bullet> _bulletPool;

    void Awake()
    {
        _bulletPool = new Queue<Bullet>(_bulletPoolSize);

        for (int i = 0; i < _bulletPoolSize; i++)
        {
            GameObject bulletObj = Instantiate(_bulletPrefab, transform);
            bulletObj.SetActive(false);
            Bullet bullet = bulletObj.GetComponent<Bullet>();
            _bulletPool.Enqueue(bullet);
        }
    }

    // 파라미터 받아서 스폰 위치 결정 및 Bullet Init에 데이터 전달
    public void Spawn(Vector3 spawnPos, Vector3 dir, float speed, float dmg, int pierce, float knockback, float range, Bullet.BulletColor color, Bullet.BulletSize size)
    {
        Bullet bullet = GetBulletFromPool();

        bullet.transform.position = spawnPos;
        bullet.transform.rotation = Quaternion.LookRotation(dir);
        bullet.gameObject.SetActive(true);

        bullet.Init(dir, speed, dmg, pierce, knockback, range, color, size, this);
    }

    private Bullet GetBulletFromPool()
    {
        if (_bulletPool.Count == 0)
        {
            GameObject bulletObj = Instantiate(_bulletPrefab, transform);
            bulletObj.SetActive(false);
            Bullet bul = bulletObj.GetComponent<Bullet>();
            _bulletPool.Enqueue(bul);
        }

        Bullet bullet = _bulletPool.Dequeue();
        return bullet;
    }

    public void Despawn(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        _bulletPool.Enqueue(bullet);
    }
}
