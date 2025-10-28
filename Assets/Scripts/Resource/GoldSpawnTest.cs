using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSpawnTest : MonoBehaviour
{
    [SerializeField] private GoldSpawner _spawner;

    [Tooltip("시작 시 미리 생성")]
    [SerializeField] private int _initalSpawnCount = 5;
    [SerializeField] private KeyCode _spawnKey = KeyCode.G;
    [SerializeField] private bool _autoSpawn = true;
    [SerializeField, Min(0.1f)] private float _spawnTime = 3f;

    [Tooltip("자동 스폰 최대 (0 이면 무한)")]
    [SerializeField] private int _autoSpawnMaxCount = 20;

    private Coroutine _autoCo;

    void Start()
    {
        if(_spawner == null)
        {
            return;
        }

        for(int i = 0; i < _initalSpawnCount; i++)
        {
            _spawner.Spawn();
        }

        if(_autoSpawn)
        {
            _autoCo = StartCoroutine(AutoSpawnRoutine());
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(_spawnKey))
        {
            _spawner.Spawn();
        }
    }

    private IEnumerator AutoSpawnRoutine()
    {
        int spawned = 0;

        while(_autoSpawnMaxCount <= 0 || spawned < _autoSpawnMaxCount)
        {
            yield return new WaitForSeconds(_spawnTime);

            _spawner.Spawn();
            spawned++;
        }
        _autoCo = null;
    }
}
