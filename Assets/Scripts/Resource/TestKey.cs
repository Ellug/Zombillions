using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestKey : MonoBehaviour
{
    [SerializeField] private GoldMineSpawner _spawner;
    [SerializeField] private GoldMine _deSpawn;


    [Tooltip("시작 시 미리 생성")]
    [SerializeField] private KeyCode[] _Key;



    void Start()
    {
        if(_spawner == null)
            return;
    }

    void Update()
    {
        if (Input.GetKeyDown(_Key[0]))
        {
            _spawner.Spawn();
        }

        if (Input.GetKeyDown(_Key[1]))
        {
            _deSpawn.TakeDamage(2);
        }

    }

    
}
