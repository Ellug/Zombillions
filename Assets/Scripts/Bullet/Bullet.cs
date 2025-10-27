using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;   //bullet�����鿡 Rigidbody �߰� �ؾ� ��
    [SerializeField] private float moveSpeed;
    [SerializeField] private float destroyTime;

    private GameObject bullet;

    //bullet ���� �� AddForce�� ���� �Ѿ� �̵� ����
    public void Spawn()
    {
        bullet = Instantiate(bulletPrefab);
        bullet.transform.position = gameObject.transform.position;
        Move();
        Destroy(bullet, destroyTime);       //destroyTime �Ŀ� �ı�
    }

    public void Move()
    {
        Rigidbody rd = bullet.GetComponent<Rigidbody>();
        rd.AddForce(100 * moveSpeed * Vector3.forward);
    }
}
