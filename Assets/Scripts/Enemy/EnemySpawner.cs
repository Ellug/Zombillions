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
            if (spawnPoint == null) Debug.LogError("Spawn Point�� �Ҵ���� �ʾҽ��ϴ�.");
            if (waves.Count == 0) Debug.LogWarning("���̺� ������ �����ϴ�.");
            if (ObjectManager.Instance == null) Debug.LogError("ObjectManager�� ���� �����ϴ�.");
            return;
        }
        StartCoroutine(WaveManagerRoutine());
    }

    [ContextMenu("Test Spawn Enemies")]
    public void TestSpawnCurrentWaveEnemies()
    {
        if (waves.Count == 0 || _currentWaveIndex >= waves.Count)
        {
            Debug.LogWarning("Test Spawn: ���̺� ������ ���ų� ���̺갡 �������ϴ�.");
            return;
        }

        Wave currentWave = waves[_currentWaveIndex];
        StartCoroutine(SpawnWaveEnemies(currentWave));

        Debug.Log($"[TEST] ���̺� {_currentWaveIndex + 1}�� ��� ���� ������ ��� �����մϴ�.");
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

        Debug.Log($"[TEST] ���� ���� ({testTag})�� ��� �����߽��ϴ�.");
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
