using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyBase : MonoBehaviour
{
    //[SerializeField] float _EnemyHP = 10f;
    [SerializeField] float _EnemyMoveSpeed = 10f;
    //[SerializeField] float _EnemayDMG = 5f;
    [SerializeField] float _ViewRange = 5f;
    private Transform _targetTransform;
    private SphereCollider _sphereCollider;
    public string poolTag = "Enemy";
    private bool _Chase = false;
    void Start()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        _sphereCollider.radius = _ViewRange;
    }

    void Update()
    {
        if (_Chase == true)
        {
            if (_targetTransform != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetTransform.position, _EnemyMoveSpeed * Time.deltaTime);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tower"))
        {
            Debug.Log("Tower OnTriggerEnter");
            _targetTransform = other.transform;
            _Chase = true;
        }
        else if (other.CompareTag("Player"))
        {
            Debug.Log("Player OnTriggerEnter");
            _targetTransform = other.transform;
            _Chase = true;
        }
        else if (other.CompareTag("HQ")) 
        {
            Debug.Log("HQ OnTriggerEnter");
            _targetTransform = other.transform;
            _Chase = true;
        }
    }
}
