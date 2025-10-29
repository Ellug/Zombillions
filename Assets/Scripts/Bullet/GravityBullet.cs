using UnityEngine;

public class GravityBullet : MonoBehaviour
{
    [SerializeField] private float _speed = 60f;
    [SerializeField] private float _maxDistance = 100f;
    [SerializeField] private float _explosionRadius = 10f;
    [SerializeField] private float _dmg = 20f;
    [SerializeField] private GameObject _explosionPrefab;

    private Vector3 _dir;
    private Vector3 _startPos;
    private bool _isActive = false;

    public void Init(Vector3 dir, float dmg, float range)
    {
        _dir = dir.normalized;
        _dmg = dmg;
        _maxDistance = range;
        _startPos = transform.position;
        _isActive = true;
    }

    void Update()
    {
        if (!_isActive) return;

        transform.Translate(_dir * _speed * Time.deltaTime, Space.World);

        // 거리 도달시 폭발
        if (Vector3.Distance(_startPos, transform.position) >= _maxDistance)
            Explode();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
            Explode();
    }

    private void Explode()
    {
        _isActive = false;

        // 중력장 생성
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
