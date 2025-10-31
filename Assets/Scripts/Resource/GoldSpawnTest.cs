using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSpawnTest : MonoBehaviour
{
    [SerializeField] private GoldSpawner _spawner;
    // 스폰 담당 컴포넌트 

    [Tooltip("시작 시 미리 생성")]
    [SerializeField][Range(0,5)] private int _initalSpawnCount = 0;
    // 시작하자마자 선 스폰 개수

    [SerializeField] private KeyCode _spawnKey = KeyCode.G;
    // G 키를 누르면 스폰되게 구현할 의도

    [SerializeField] private bool _autoSpawn = true;
    [SerializeField, Min(0.1f)] private float _spawnTime = 3f;
    // 자동 스폰 사용 여부 min 으로 0 이하 설정 방지

    [Tooltip("자동 스폰 최대 (0 이면 무한)")]
    [SerializeField] private int _autoSpawnMaxCount = 20;
    // 자동 스폰 총 횟수 제한 ( 0 은 무제한 )

    private Coroutine _autoCo;
    // 게임 흐름을 관리할 코루틴 선언

    void Start()
    {
        if(_spawner == null)
        {
            return;
        }
        // 스포너 미연결이면 아무 작업도 하지 않음

        for(int i = 0; i < _initalSpawnCount; i++)
        {
            _spawner.Spawn();
        }
        // 선 스폰 루프

        if(_autoSpawn)
        {
            _autoCo = StartCoroutine(AutoSpawnRoutine());
        }
        // 자동 스폰 활성화 시 코루틴 시작 ( 반복 타이머 )
    }

    void Update()
    {
        if(Input.GetKeyDown(_spawnKey))
        {
            _spawner.Spawn();
        }
    }
    // 키를 눌렀을 때 즉시 1회 스폰
    // 

    private IEnumerator AutoSpawnRoutine()
    {
        int spawned = 0;
        // 누적 수를 카운트

        while(_autoSpawnMaxCount <= 0 || spawned < _autoSpawnMaxCount)
        {
            yield return new WaitForSeconds(_spawnTime);

            _spawner.Spawn();
            spawned++;
        }
        _autoCo = null;
    }
}
