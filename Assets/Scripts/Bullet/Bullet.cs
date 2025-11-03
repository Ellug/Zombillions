using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum BulletColor { Red, Yellow, Metal }
    public enum BulletSize { Small, Medium, Large }

    private Vector3 _dir;
    private float _speed;
    private float _dmg;
    private int _penetration;
    private float _knockback;
    private float _maxDistance;
    private Vector3 _startPos;
    private BulletColor _color;
    private BulletSize _size;
    private BulletSpawner _spawner;
    private Transform _AttackerTransform;

    // 파라미터 받아서 총알에 속성 부여 (방향, 속도, 대미지 등등 받아서 사용)
    public void Init(Vector3 dir, float speed, float dmg, int pierce, float knockback, float range, BulletColor color, BulletSize size, Transform AttackerTransform, BulletSpawner spawner)
    {
        _dir = dir.normalized;
        _speed = speed;
        _dmg = dmg;
        _penetration = pierce;
        _knockback = knockback;
        _maxDistance = range;
        _color = color;
        _size = size;
        _spawner = spawner;
        _startPos = transform.position;
        _AttackerTransform = AttackerTransform;

        SetupRender();
    }

    void Update()
    {
        Move();
    }

    // 총알 영역 트리거시 작동
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyBase enemy = other.GetComponent<EnemyBase>();
            enemy.TakeDamage(_dmg, _AttackerTransform);

            // 넉백 (Transform 이동)
            Vector3 dir = (enemy.transform.position - _startPos).normalized;
            Vector3 newPos = enemy.transform.position + dir * _knockback;
            enemy.transform.position = newPos;

            _penetration--;
            
            if (_penetration <= 0)
                _spawner.Despawn(this);
        }
    }

    // 총알 이동
    public void Move()
    {
        transform.Translate(_dir * _speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(_startPos, transform.position) >= _maxDistance)
            _spawner.Despawn(this);
    }

    // 총알의 렌더러를 변경해서 프리팹 여러개 만들지 않고도 다양한 총알처럼 표현 가능
    private void SetupRender()
    {
        var renderer = GetComponent<MeshRenderer>();
        if (renderer == null) return;

        switch (_color)
        {
            case BulletColor.Red:
                renderer.material.color = Color.red;
                break;
            case BulletColor.Yellow:
                renderer.material.color = Color.yellow;
                break;
            case BulletColor.Metal:
                renderer.material.color = Color.gray;
                break;
        }

        switch (_size)
        {
            case BulletSize.Small:
                transform.localScale = Vector3.one * 0.3f;
                break;
            case BulletSize.Medium:
                transform.localScale = Vector3.one * 1f;
                break;
            case BulletSize.Large:
                transform.localScale = Vector3.one * 2f;
                break;
        }
    }
}
