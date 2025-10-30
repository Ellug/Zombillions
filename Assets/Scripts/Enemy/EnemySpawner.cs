using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField][Range(0, 30)] private float _PositionScope;
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

    private Vector3 RandPos() 
    {
        Vector3 pos = new Vector3
            (
            Random.Range(-_PositionScope, _PositionScope),
            0f,
            Random.Range(-_PositionScope, _PositionScope)
            );
        return pos;
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
                spawnPoint.position + RandPos(),
                transform.rotation
            );
            yield return new WaitForSeconds(enemyInfo.spawnInterval);
        }
    }
}
