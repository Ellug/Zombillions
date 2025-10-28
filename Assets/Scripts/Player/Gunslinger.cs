using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunslinger : PlayerBase
{
    [SerializeField] private float _atkRange;

    protected override void Attack()
    {
        Debug.Log("총 공격");
    }
}
