using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class WaveEnemyInfo
    {
        public string enemyPoolTag;
        public int count;
        public float spawnInterval = 1f;
    }
    [System.Serializable]
    public class Wave
    {
        public float duration = 600f;
        public List<WaveEnemyInfo> waveEnemies;
    }

    public List<Wave> waves;

    public Transform spawnPoint;

    private int _currentWaveIndex = 0;

    void Start()
    {
        if (spawnPoint == null || waves.Count == 0 || ObjectManager.Instance == null)
        {
            if (spawnPoint == null) Debug.LogError("Spawn Point가 할당되지 않았습니다.");
            if (waves.Count == 0) Debug.LogWarning("웨이브 정보가 없습니다.");
            if (ObjectManager.Instance == null) Debug.LogError("ObjectManager가 씬에 없습니다.");
            return;
        }
        StartCoroutine(WaveManagerRoutine());
    }

    [ContextMenu("Test Spawn Enemies")]
    public void TestSpawnCurrentWaveEnemies()
    {
        if (waves.Count == 0 || _currentWaveIndex >= waves.Count)
        {
            Debug.LogWarning("Test Spawn: 웨이브 정보가 없거나 웨이브가 끝났습니다.");
            return;
        }

        Wave currentWave = waves[_currentWaveIndex];
        StartCoroutine(SpawnWaveEnemies(currentWave));

        Debug.Log($"[TEST] 웨이브 {_currentWaveIndex + 1}의 모든 몬스터 스폰을 즉시 시작합니다.");
    }

    [ContextMenu("Test Spawn Single Enemy (Worker)")]
    public void TestSpawnSingleWorker()
    {
        string testTag = "WorkerPool";
        ObjectManager.Instance.SpawnFromPool(
            testTag,
            spawnPoint.position,
            spawnPoint.rotation
        );

        Debug.Log($"[TEST] 단일 몬스터 ({testTag})를 즉시 스폰했습니다.");
    }
    IEnumerator WaveManagerRoutine()
    {
        while (_currentWaveIndex < waves.Count)
        {
            Wave currentWave = waves[_currentWaveIndex];
            StartCoroutine(SpawnWaveEnemies(currentWave));
            yield return new WaitForSeconds(currentWave.duration);
            _currentWaveIndex++;
        }
    }
    IEnumerator SpawnWaveEnemies(Wave wave)
    {
        foreach (WaveEnemyInfo enemyInfo in wave.waveEnemies)
        {
            StartCoroutine(SpawnSpecificEnemy(enemyInfo));
        }

        yield break;
    }
    IEnumerator SpawnSpecificEnemy(WaveEnemyInfo enemyInfo)
    {
        for (int i = 0; i < enemyInfo.count; i++)
        {
            ObjectManager.Instance.SpawnFromPool(
                enemyInfo.enemyPoolTag,
                spawnPoint.position,
                spawnPoint.rotation
            );
            yield return new WaitForSeconds(enemyInfo.spawnInterval);
        }
    }
}
