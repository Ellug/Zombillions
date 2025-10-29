using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private Transform _muzzle;
    [SerializeField] private GameObject _bulletPrefab;   //bullet�����鿡 Rigidbody �߰� �ؾ� ��
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


    //bullet ���� �� AddForce�� ���� �Ѿ� �̵� ����
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
