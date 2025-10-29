using System.Collections.Generic;
using UnityEngine;


public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private int _bulletPoolSize = 30;

    private Queue<Bullet> _bulletPool;

    // 다른 요소들 Start 보다 먼저 초기화 보장을 위해 Awake로 수정
    // 위치 등의 초기화는 스포너가 아니라 Bullet에게 책임 이동
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

        // 이러면 빈 List 문제도 대처 가능하기에 해당 검증도 생략 가능
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

    // 비활성화 된 bullet을 찾아 순회해야하는 코드
    // List 구조이기 때문

    // private Bullet GetInactiveBullet()
    // {
    //     foreach (var bullet in _bulletPool)
    //     {
    //         // 동일한 조건이 두개 있어서 정리            
    //         if (!bullet.gameObject.activeSelf)
    //             return bullet;
    //     }
    //     return null;
    // }

    // Queue로 구현하면 해당 부분을 최적화 가능
    private Bullet GetBulletFromPool()
    {
        // 풀에 비활성화된 탄이 없는 경우 새로 생성
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
