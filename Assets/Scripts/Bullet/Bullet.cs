using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _damage;
    [SerializeField] private float _deactiveTime;
    [SerializeField] private float _shotForce;

    private Rigidbody _rigidbody;
    private float _deactCount;

    void Awake()
    {
        Init();        
    }

    void OnEnable()
    {
        Move();
    }

    void Update()
    {
        TimeCount();   
    }

    private void Init()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void TimeCount()
    {
        _deactCount -= Time.deltaTime;
        if (_deactCount < 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void Move()
    {
        _deactCount = _deactiveTime;
        _rigidbody.AddForce(transform.forward * _shotForce, ForceMode.Impulse);
    }

   
}
