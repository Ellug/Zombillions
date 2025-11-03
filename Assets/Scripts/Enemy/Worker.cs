using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : EnemyBase
{
    protected override void Start()
    {
        _poolTag = "Worker";
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    // protected override void OnTriggerEnter(Collider other)
    // {
    //     base.OnTriggerEnter(other);
    // }
}
