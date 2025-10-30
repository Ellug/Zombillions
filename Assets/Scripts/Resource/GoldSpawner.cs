using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 금광을 생성 / 회수하는 스포너
/// 오브젝트 풀 구현
/// </summary>
public class GoldSpawner : MonoBehaviour
{
    [Header("Prefab & PoolSize")]
    [Tooltip("소환할 Gold Prefab")]
    [SerializeField] private Gold _goldPrefab;

    [Tooltip("초기 PoolSize")]
    [SerializeField] private int _initialPoolSize = 20;

    [Header("스폰 방식")]
    [Tooltip("True : 랜덤 범위 스폰 / False : 스폰 포인트 배열에서 뽑아 스폰")]
    [SerializeField] private bool _useRandomArea = false;

    [Tooltip("랜덤 스폰 범위")]
    [SerializeField] private Vector3 _randomAreaSize = new Vector3(20f, 0f, 20f);

    [Tooltip("고정 스폰")]
    [SerializeField] private Transform[] _spawnPoints;

    private Queue<Gold> _pool = new Queue<Gold>();
    private List<Gold> _actives = new List<Gold>();

    void Awake()
    {
        if(_goldPrefab == null)
        {
            Debug.LogError("Gold Prefab 미할당");
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
        // 풀에서 꺼내오거나 없으면 새로 생성
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

        if(_actives.Contains(target)) // 중복 회수 방지
        {
            _actives.Remove(target);
        }

        // 비활성화 후 부모를 스포너로 되돌림
        target.gameObject.SetActive(false);
        target.transform.SetParent(transform, worldPositionStays: false);

        _pool.Enqueue(target);
    }

    private Vector3 GetSpawnPosition()
    {
        if(_useRandomArea) // 랜덤 스폰일때
        {
            Vector3 half = _randomAreaSize * 0.5f;

            float x = Random.Range(-half.x, half.x);
            float z = Random.Range(-half.z, half.z);
            float y = 0f;

            Vector3 local = new Vector3(x, y, z);
            Vector3 world = transform.TransformPoint(local);
            return world;
        }
        else // 고정 스폰일때
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
