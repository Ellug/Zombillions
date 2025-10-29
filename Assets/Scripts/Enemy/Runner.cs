using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : EnemyBase
{
    protected override void Start()
    {
        _poolTag = "Runner";
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
