using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 금광을 생성 / 회수하는 스포너
/// 오브젝트 풀 구현
/// </summary>
public class GoldMineSpawner : MonoBehaviour , ITimeObserver
{
    [Header("Prefab & PoolSize")]
    [Tooltip("소환할 Gold Prefab")]
    [SerializeField] private GoldMine _goldMinePrefab;

    [Tooltip("초기 PoolSize")]
    [SerializeField] private int _poolSize = 50;

    [Tooltip("랜덤 스폰 범위")]
    [SerializeField] private Vector3 _randomAreaSize = new Vector3(10f, 0f, 10f);

    [Tooltip("SpawnPoint 리스트")]
    [SerializeField] private Transform[] _spawnPoints;

    [SerializeField] private float _spawnTime = 1f;

    private GlobalTime _globalTime;
    private Queue<GoldMine> _goldMinePool = new();
    private List<GoldMine> _activeGoldMine = new();

    void Awake()
    {
        if (_goldMinePrefab == null)
        {
            Debug.LogError("Gold Prefab 미할당");
            return;
        }

        for (int i = 0; i < _poolSize; i++)
        {
            GoldMine created = CreateNew();
            _goldMinePool.Enqueue(created);
        }
        _globalTime = GameManager.Instance.Timer;
        _globalTime.AddObserver(this);
    }


    void Start()
    {
        StartCoroutine(SpawnTime(Day.Noon));
    }


    // 초기 골드 생성 메서드
    private GoldMine CreateNew()
    {
        GoldMine instance = Instantiate(_goldMinePrefab, transform);
        instance.gameObject.SetActive(false);
        return instance;
    }

    public void Spawn(Vector3? worldPosition = null, Quaternion? worldRotation = null)
    {
        GoldMine instance = _goldMinePool.Dequeue();

        Vector3 spawnPos;
        Quaternion spawnRot;

        // 위치 결정 (입력 없을 시 랜덤/고정 스폰값 지정)
        if (worldPosition.HasValue)
            spawnPos = worldPosition.Value;
        else
            spawnPos = GetSpawnPosition();

        // 회전 결정 (입력 없을 시 랜덤/고정 스폰값 지정)
        if (worldRotation.HasValue)
            spawnRot = worldRotation.Value;
        else
            spawnRot = Quaternion.identity;

        instance.transform.SetPositionAndRotation(spawnPos, spawnRot);
        instance.gameObject.SetActive(true);
        _activeGoldMine.Add(instance);
    }

    public void Despawn(GoldMine gold)
    {
        if (gold == null)
            return;

        gold.gameObject.SetActive(false);
        gold.Init();
        _goldMinePool.Enqueue(gold);
    }

    // 스폰포인트 골라서 GoldMine 스폰
    private Vector3 GetSpawnPosition()
    {
            if (_spawnPoints == null || _spawnPoints.Length == 0)
                return transform.position;

            Vector3 range = _randomAreaSize;
            float x = Random.Range(-range.x, range.x);
            float z = Random.Range(-range.z, range.z);
            float y = 0f;

            int index = Random.Range(0, _spawnPoints.Length);
            Transform spawnPoint = _spawnPoints[index];

            Vector3 local = new Vector3(x, y, z);
            Vector3 world = _spawnPoints[index].TransformPoint(local);
            return world;
    }

    // 낮/밤의 변화에 따른 변화
    public void OnTimeZoneChange(Day newTimeZone)
    {
        if(newTimeZone == Day.Noon)
            StartCoroutine(SpawnTime(newTimeZone));

        // 밤이 될 경우, 모든 GoldMine 사라짐
        if(newTimeZone == Day.Night)
        {
            StopAllCoroutines();
            foreach (var gold in _activeGoldMine)
            {
                gold.Init();
                gold.gameObject.SetActive(false);
                _goldMinePool.Enqueue(gold);
            }
        }
    }

    // 낮 시간 랜덤 포인트 스폰
    private IEnumerator SpawnTime(Day TimeZone)
    {
        while (TimeZone == Day.Noon)
        {
            Spawn();
            yield return new WaitForSeconds(_spawnTime);
        }
    }

}
