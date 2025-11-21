using System.Collections;
using UnityEngine;

public class GravityField : MonoBehaviour
{
    private float _duration = 10f;
    private float _radius = 25f;
    private float _gravityForce = 15f;
    float _forceInterval = 0.05f;

    private float _forceTimer;
    private Collider[] _hitsBuffer = new Collider[50];
    private WaitForSeconds _wait;

    void Start()
    {
        _wait = new WaitForSeconds(_forceInterval);
        StartCoroutine(GravityEffect());
    }

    private IEnumerator GravityEffect()
    {
        _forceTimer = 0f;

        while (_forceTimer < _duration)
        {
            _forceTimer += _forceInterval;
            // 영역 설정 및 끌어오기
            int count = Physics.OverlapSphereNonAlloc(transform.position, _radius, _hitsBuffer);
            for (int i = 0; i< count; i++)
            {
                var hit = _hitsBuffer[i];
                if (!hit.CompareTag("Enemy")) continue;

                Transform enemyTr = hit.transform;

                Vector3 dir = transform.position - enemyTr.position;
                dir.y = 0f;
                dir.Normalize();

                // 거리 기반 중력 감소 (멀수록 약하게)
                float distance = Vector3.Distance(transform.position, enemyTr.position);
                float pullForce = Mathf.Lerp(_gravityForce, 0f, distance / _radius);

                // 끌어당김(포지션 이동)
                Vector3 move = pullForce * _forceInterval * dir;
                enemyTr.position += move;

                var enemy = hit.GetComponent<EnemyBase>();
                if (enemy != null)
                    enemy.TakeDamage(0.1f, null);
            }

            yield return _wait;
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
