using System.Collections.Generic;
using UnityEngine;


public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private int _bulletPoolSize = 30;

    private Queue<Bullet> _bulletPool;

    // �ٸ� ��ҵ� Start ���� ���� �ʱ�ȭ ������ ���� Awake�� ����
    // ��ġ ���� �ʱ�ȭ�� �����ʰ� �ƴ϶� Bullet���� å�� �̵�
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

    // �Ķ���� �޾Ƽ� ���� ��ġ ���� �� Bullet Init�� ������ ����
    public void Spawn(Vector3 spawnPos, Vector3 dir, float speed, float dmg, int pierce, float knockback, float range, Bullet.BulletColor color, Bullet.BulletSize size)
    {
        Bullet bullet = GetBulletFromPool();

        // �̷��� �� List ������ ��ó �����ϱ⿡ �ش� ������ ���� ����
        // if (bullet == null)
        // {
        //     GameObject bulletObj = Instantiate(_bulletPrefab, transform);
        //     bullet = bulletObj.GetComponent<Bullet>();
        //     _bulletPool.Add(obj.GetComponent<Bullet>());
        // }

        bullet.transform.position = spawnPos;
        bullet.transform.rotation = Quaternion.LookRotation(dir);
        bullet.gameObject.SetActive(true);

        bullet.Init(dir, speed, dmg, pierce, knockback, range, color, size, this);
    }

    // ��Ȱ��ȭ �� bullet�� ã�� ��ȸ�ؾ��ϴ� �ڵ�
    // List �����̱� ����

    // private Bullet GetInactiveBullet()
    // {
    //     foreach (var bullet in _bulletPool)
    //     {
    //         // ������ ������ �ΰ� �־ ����            
    //         if (!bullet.gameObject.activeSelf)
    //             return bullet;
    //     }
    //     return null;
    // }

    // Queue�� �����ϸ� �ش� �κ��� ����ȭ ����
    private Bullet GetBulletFromPool()
    {
        // Ǯ�� ��Ȱ��ȭ�� ź�� ���� ��� ���� ����
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
