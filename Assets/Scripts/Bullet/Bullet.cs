using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;   //bullet프리펩에 Rigidbody 추가 해야 함
    [SerializeField] private float moveSpeed;
    [SerializeField] private float destroyTime;

    private GameObject bullet;

    //bullet 생성 후 AddForce를 통해 총알 이동 구현
    public void Spawn()
    {
        bullet = Instantiate(bulletPrefab);
        bullet.transform.position = gameObject.transform.position;
        Move();
        Destroy(bullet, destroyTime);       //destroyTime 후에 파괴
    }

    public void Move()
    {
        Rigidbody rd = bullet.GetComponent<Rigidbody>();
        rd.AddForce(100 * moveSpeed * Vector3.forward);
    }
}
