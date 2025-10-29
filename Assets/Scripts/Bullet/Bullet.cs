using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _range;
    [SerializeField] private float _damage;
    [SerializeField] private int _penetration = 1;

    private Vector3 _travelVector;      //총알이 이동한 거리 계산을 위한 벡터


    void Update()
    {
        Move();
        DeactiveDistance();
    }

    //총알 비활성화 메서드
    private void DeactiveDistance()
    {
        float sqrmagnitude = Vector3.SqrMagnitude(_travelVector);
        if (sqrmagnitude >= _range * _range)
        {
            gameObject.SetActive(false);
        }
    }

    //총알 관통력이 0이면 소멸하는 메서드
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
            _penetration--;
        if (_penetration == 0)
            gameObject.SetActive(false);
    }

    public void Move()
    {
        if (Vector3.SqrMagnitude(_travelVector) >= _range * _range)   //_travelVector의 길이가 사정거리보다 큰 상태라면 초기화
            _travelVector = Vector3.zero;

        Vector3 direction = Time.deltaTime * _moveSpeed * Vector3.forward.normalized;

        this.transform.Translate(direction , Space.Self);
        _travelVector += direction;
        

    }
}
