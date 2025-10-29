using System.Collections;
using UnityEngine;

public class GravityField : MonoBehaviour
{
    private float _duration = 10f;
    private float _radius = 15f;
    private float _gravityForce = 20f;
    private LayerMask _enemyMask;
    float _forceInterval = 0.1f;

    private float _forceTimer;

    void Start()
    {
        StartCoroutine(GravityEffect());
    }

    private IEnumerator GravityEffect()
    {
        _forceTimer = 0f;

        while (_forceTimer < _duration)
        {
            _forceTimer += _forceInterval;
            // 영역 설정 및 끌어오기
            Collider[] hits = Physics.OverlapSphere(transform.position, _radius);
            foreach (var hit in hits)
            {
                if (!hit.CompareTag("Enemy")) continue;

                // var enemy = hit.GetComponent<EnemyBase>();
                // if (enemy != null)
                //     enemy.TakeDamage(_dmg * 0.5f);
            }

            yield return new WaitForSeconds(_forceInterval);
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
