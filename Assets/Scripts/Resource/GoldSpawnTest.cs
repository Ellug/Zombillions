using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSpawnTest : MonoBehaviour
{
    [SerializeField] private GoldSpawner _spawner;
    // ���� ��� ������Ʈ 

    [Tooltip("���� �� �̸� ����")]
    [SerializeField][Range(0,5)] private int _initalSpawnCount = 0;
    // �������ڸ��� �� ���� ����

    [SerializeField] private KeyCode _spawnKey = KeyCode.G;
    // G Ű�� ������ �����ǰ� ������ �ǵ�

    [SerializeField] private bool _autoSpawn = true;
    [SerializeField, Min(0.1f)] private float _spawnTime = 3f;
    // �ڵ� ���� ��� ���� min ���� 0 ���� ���� ����

    [Tooltip("�ڵ� ���� �ִ� (0 �̸� ����)")]
    [SerializeField] private int _autoSpawnMaxCount = 20;
    // �ڵ� ���� �� Ƚ�� ���� ( 0 �� ������ )

    private Coroutine _autoCo;
    // ���� �帧�� ������ �ڷ�ƾ ����

    void Start()
    {
        if(_spawner == null)
        {
            return;
        }
        // ������ �̿����̸� �ƹ� �۾��� ���� ����

        for(int i = 0; i < _initalSpawnCount; i++)
        {
            _spawner.Spawn();
        }
        // �� ���� ����

        if(_autoSpawn)
        {
            _autoCo = StartCoroutine(AutoSpawnRoutine());
        }
        // �ڵ� ���� Ȱ��ȭ �� �ڷ�ƾ ���� ( �ݺ� Ÿ�̸� )
    }

    void Update()
    {
        if(Input.GetKeyDown(_spawnKey))
        {
            _spawner.Spawn();
        }
    }
    // Ű�� ������ �� ��� 1ȸ ����
    // 

    private IEnumerator AutoSpawnRoutine()
    {
        int spawned = 0;
        // ���� ���� ī��Ʈ

        while(_autoSpawnMaxCount <= 0 || spawned < _autoSpawnMaxCount)
        {
            yield return new WaitForSeconds(_spawnTime);

            _spawner.Spawn();
            spawned++;
        }
        _autoCo = null;
    }
}
