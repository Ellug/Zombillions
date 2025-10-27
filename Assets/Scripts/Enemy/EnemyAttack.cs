using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private EnemyBase _enemyBase;
    void Awake()
    {
        _enemyBase = GetComponentInParent<EnemyBase>();
        if (_enemyBase == null)
        {
            Debug.LogError("�θ�� EnemyBase.cs �� �����ϴ�.");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tower"))
        {
            Debug.Log("Tower Attack OnTriggerEnter");
        }
        else if (other.CompareTag("Player"))
        {
            Debug.Log("Player Attack OnTriggerEnter");
        }
        else if (other.CompareTag("HQ"))
        {
            Debug.Log("HQ Attack OnTriggerEnter");
        }
    }
}
