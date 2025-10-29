using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPBar : MonoBehaviour
{
    private Transform MainCamera;
    private EnemyBase _enemyBase;
    void Start()
    {
        MainCamera = Camera.main.transform;
    }
    void Update()
    {
        transform.LookAt(transform.position + MainCamera.rotation * Vector3.forward, MainCamera.rotation * Vector3.up);
    }
}
