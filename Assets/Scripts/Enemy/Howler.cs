using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Howler : EnemyBase
{
    [SerializeField] public string pooltag = "Howler";
    [SerializeField] private float _HowlingRange = 20f;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    private void Howling() 
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _HowlingRange);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy") && hitCollider.gameObject != this.gameObject)
            {
                EnemyBase NearEnemy = hitCollider.GetComponent<EnemyBase>();

                if (NearEnemy != null && !NearEnemy._Chase)
                {
                    NearEnemy._Chase = true;
                    if (_targetTransform != null)
                    {
                        NearEnemy._targetTransform = _targetTransform;
                    }
                }
            }
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (_Chase) 
        {
            Howling();
        }
    }
}
