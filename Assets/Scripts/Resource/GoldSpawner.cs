using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ݱ��� ���� / ȸ���ϴ� ������
/// ������Ʈ Ǯ ����
/// </summary>
public class GoldSpawner : MonoBehaviour
{
    [Header("Prefab & PoolSize")]
    [Tooltip("��ȯ�� Gold Prefab")]
    [SerializeField] private Gold _goldPrefab;

    [Tooltip("�ʱ� PoolSize")]
    [SerializeField] private int _initialPoolSize = 20;

    [Header("���� ���")]
    [Tooltip("True : ���� ���� ���� / False : ���� ����Ʈ �迭���� �̾� ����")]
    [SerializeField] private bool _useRandomArea = false;

    [Tooltip("���� ���� ����")]
    [SerializeField] private Vector3 _randomAreaSize = new Vector3(20f, 0f, 20f);

    [Tooltip("���� ����")]
    [SerializeField] private Transform[] _spawnPoints;

    private Queue<Gold> _pool = new Queue<Gold>();
    private List<Gold> _actives = new List<Gold>();

    void Awake()
    {
        if(_goldPrefab == null)
        {
            Debug.LogError("Gold Prefab ���Ҵ�");
            return;
        }

        for(int i = 0; i < _initialPoolSize; i++)
        {
            Gold created = CreateNew();
            _pool.Enqueue(created);
        }
    }

    private Gold CreateNew()
    {
        Gold instance = Instantiate(_goldPrefab, GetSpawnPosition(), Quaternion.identity);
        instance.gameObject.SetActive(false);
        instance.SetOwner(this);
        return instance;
    }

    public Gold Spawn(Vector3? worldPosition = null, Quaternion? worldRotation = null)
    {
        // Ǯ���� �������ų� ������ ���� ����
        Gold instance;
        if(_pool.Count > 0)
        {
            instance = _pool.Dequeue();
        }
        else
        {
            instance = CreateNew();
        }

        Vector3 spawnPos;
        Quaternion spawnRot;

        if(worldPosition.HasValue)
        {
            spawnPos = worldPosition.Value;
        }
        else
        {
            spawnPos = GetSpawnPosition();
        }

        if(worldRotation.HasValue)
        {
            spawnRot = worldRotation.Value;
        }
        else
        {
            spawnRot = Quaternion.identity;
        }

        Transform t = instance.transform;
        t.SetPositionAndRotation(spawnPos, spawnRot);

        instance.gameObject.SetActive(true);

        return instance;
    }

    public void Despawn(Gold target)
    {
        if(target == null)
        {
            return;
        }

        if(_actives.Contains(target)) // �ߺ� ȸ�� ����
        {
            _actives.Remove(target);
        }

        // ��Ȱ��ȭ �� �θ� �����ʷ� �ǵ���
        target.gameObject.SetActive(false);
        target.transform.SetParent(transform, worldPositionStays: false);

        _pool.Enqueue(target);
    }

    private Vector3 GetSpawnPosition()
    {
        if(_useRandomArea) // ���� �����϶�
        {
            Vector3 half = _randomAreaSize * 0.5f;

            float x = Random.Range(-half.x, half.x);
            float z = Random.Range(-half.z, half.z);
            float y = 0f;

            Vector3 local = new Vector3(x, y, z);
            Vector3 world = transform.TransformPoint(local);
            return world;
        }
        else // ���� �����϶�
        {
            if(_spawnPoints == null || _spawnPoints.Length == 0)
            {
                return transform.position;
            }

            int index = Random.Range(0, _spawnPoints.Length);
            Transform p = _spawnPoints[index];
            return (p != null) ? p.position : transform.position;
        }
    }
}
