using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum BulletColor { Red, Yellow, Metal }
    public enum BulletSize { Small, Medium, Large }

    // ����ȭ X, �����ʿ� ���ؼ��� �����ǵ��� ����
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

    // �Ķ���� �޾Ƽ� �Ѿ˿� �Ӽ� �ο� (����, �ӵ�, ����� ��� �޾Ƽ� ���. ��ü ����X)
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

        // ��� ���� �Լ� ȣ��
        SetupRender();
    }

    void Update()
    {
        Move();
    }

    // �Ѿ� ���� Ʈ���Ž� �۵�
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyBase enemy = other.GetComponent<EnemyBase>();
            enemy.TakeDamage(_dmg);
            // �Ѿ˰� ���׹� �߽� ���� ���� �������� �˹� �ο�. �˹� �޼���� ���׹̿��� ���� �ʿ�
            // enemy.Knockback((enemy.transform.position - transform.position).normalized * _knockback);

            _penetration--;

            // �̷� ��� == �� ����ϸ� ������ ���� �۵� ���� �� �־� �����ϰ� <=�� ó�� ����
            if (_penetration <= 0)
                _spawner.Despawn(this);
        }
    }

    // �Ÿ� ��� ��Ȱ��ȭ�� �ߺ�ó�� ���־� �����ϰ� ����.
    public void Move()
    {
        // ������ �ܺο��� ���޹޾Ҵµ� ���� ���� ������ ����� ������ ���� ���� World�� ����
        transform.Translate(_dir * _speed * Time.deltaTime, Space.World);

        // �������� ���� ���� �Ÿ��� >= _maxDistance ���� ũ�� ��Ȱ��ȭ�� Magnitude ��꺸�� ������ ó�� ����
        if (Vector3.Distance(_startPos, transform.position) >= _maxDistance)
            _spawner.Despawn(this);
    }

    // �Ѿ��� �������� �����ؼ� ������ ������ ������ �ʰ� �پ��� �Ѿ�ó�� ǥ�� ����
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
