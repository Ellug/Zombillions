using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyWave : MonoBehaviour
{
    [SerializeField] public float _WaveRange = 30f;
    [SerializeField] GameObject targetObject;
    [SerializeField] public bool WaveTest = false;
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
                    NearEnemy._Chase = true;
                }
            }
        }
    }
    void Start()
    {
        StartCoroutine(RepeatFunctionEveryTenMinutes());
    }

    void Update()
    {
        // if (WaveTest)
        // {
        //     StartWave();
        // }
    }
    IEnumerator RepeatFunctionEveryTenMinutes()
    {
        float tenMinutes = 60.0f;
        yield return new WaitForSeconds(tenMinutes);

        while (true)
        {
            StartWave();
            yield return new WaitForSeconds(tenMinutes);
        }
    }
}
