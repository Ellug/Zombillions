using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private Transform _muzzle;
    [SerializeField] private GameObject _bulletPrefab;   //bullet프리펩에 Rigidbody 추가 해야 함
    [SerializeField] private int _BulletPoolSize = 30;


    private List<GameObject> _bulletPool;


    void Start()
    {
        _bulletPool = new();
        for (int i = 0; i < _BulletPoolSize; i++)
        {
            GameObject bullet = Instantiate(_bulletPrefab);
            bullet.transform.position = _muzzle.transform.position;
            _bulletPool.Add(bullet);
            bullet.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Spawn();
        }

    }


    //bullet 생성 후 AddForce를 통해 총알 이동 구현
    public void Spawn()
    {
        foreach (var bullet in _bulletPool)
        {
            if (!bullet.activeSelf)
            {
                bullet.transform.rotation = _muzzle.rotation;
                bullet.transform.position = _muzzle.transform.position;
                bullet.SetActive(true);
                break;
            }

        }
    }
}
