using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum BulletColor { Red, Yellow, Metal }
    public enum BulletSize { Small, Medium, Large }

    // 직렬화 X, 스포너에 의해서만 결정되도록 구성
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

    // 파라미터 받아서 총알에 속성 부여 (방향, 속도, 대미지 등등 받아서 사용. 자체 결정X)
    public void Init(Vector3 dir, float speed, float dmg, int pierce, float knockback, float range, BulletColor color, BulletSize size, BulletSpawner spawner)
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

        // 모양 변경 함수 호출
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
            enemy.TakeDamage(_dmg);
            // 총알과 에네미 중심 사이 벡터 방향으로 넉백 부여. 넉백 메서드는 에네미에서 구현 필요
            // enemy.Knockback((enemy.transform.position - transform.position).normalized * _knockback);

            _penetration--;

            // 이런 경우 == 로 계산하면 오차로 인해 작동 안할 수 있어 안전하게 <=로 처리 권장
            if (_penetration <= 0)
                _spawner.Despawn(this);
        }
    }

    // 거리 계산 비활성화가 중복처리 돼있어 간결하게 통합.
    public void Move()
    {
        // 방향을 외부에서 전달받았는데 로컬 기준 방향을 사용할 이유가 없어 보여 World로 수정
        transform.Translate(_dir * _speed * Time.deltaTime, Space.World);

        // 시작점과 현재 지점 거리가 >= _maxDistance 보다 크면 비활성화로 Magnitude 계산보다 간결히 처리 가능
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
