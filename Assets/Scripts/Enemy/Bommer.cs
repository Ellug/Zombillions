using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bommer : EnemyBase
{
    [SerializeField] public string pooltag = "Bommer";
    protected override void Start()
    {
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
