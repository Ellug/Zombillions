using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour
{
    [SerializeField] public float _WaveRange = 30f;
    [SerializeField] GameObject targetObject;
    private void StartWave()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _WaveRange);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy") && hitCollider.gameObject != this.gameObject)
            {
                EnemyBase NearEnemy = hitCollider.GetComponent<EnemyBase>();

                if (NearEnemy != null && !NearEnemy._Chase)
                {
                    NearEnemy._targetTransform = targetObject.transform;
                }
            }
        }
    }
    void Start()
    {
        StartCoroutine(RepeatFunctionEveryTenMinutes());
    }
    IEnumerator RepeatFunctionEveryTenMinutes()
    {
        float tenMinutes = 600.0f;
        while (true)
        {
            StartWave();
            yield return new WaitForSeconds(tenMinutes);
        }
    }
}
