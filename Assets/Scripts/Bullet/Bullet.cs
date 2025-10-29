using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _range;
    [SerializeField] private float _damage;
    [SerializeField] private int _penetration = 1;

    private Vector3 _travelVector;      //�Ѿ��� �̵��� �Ÿ� ����� ���� ����


    void Update()
    {
        Move();
        DeactiveDistance();
    }

    //�Ѿ� ��Ȱ��ȭ �޼���
    private void DeactiveDistance()
    {
        float sqrmagnitude = Vector3.SqrMagnitude(_travelVector);
        if (sqrmagnitude >= _range * _range)
        {
            gameObject.SetActive(false);
        }
    }

    //�Ѿ� ������� 0�̸� �Ҹ��ϴ� �޼���
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
            _penetration--;
        if (_penetration == 0)
            gameObject.SetActive(false);
    }

    public void Move()
    {
        if (Vector3.SqrMagnitude(_travelVector) >= _range * _range)   //_travelVector�� ���̰� �����Ÿ����� ū ���¶�� �ʱ�ȭ
            _travelVector = Vector3.zero;

        Vector3 direction = Time.deltaTime * _moveSpeed * Vector3.forward.normalized;

        this.transform.Translate(direction , Space.Self);
        _travelVector += direction;
        

    }
}
