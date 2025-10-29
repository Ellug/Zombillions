using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool
{
    public string tag;
    public GameObject prefab;
    public int size;
}

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager Instance;
    public List<Pool> pools;
    private Dictionary<string, Queue<GameObject>> poolDictionary;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        InitializePools();
    }
    private void InitializePools()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            GameObject parent = new GameObject(pool.tag + " Pool Parent");
            parent.transform.SetParent(this.transform);

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, parent.transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
        Debug.Log("ObjectManager: ��� ������Ʈ Ǯ �ʱ�ȭ �Ϸ�.");
    }
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogError($"[POOL ERROR] ��û�� �±� '{tag}'�� ObjectManager�� ��ϵǾ� ���� �ʽ��ϴ�.");
            string registeredTags = string.Join(", ", poolDictionary.Keys);
            Debug.LogWarning($"[POOL INFO] ���� ��ϵ� �±� ���: {registeredTags}");
            return null;
        }

        Queue<GameObject> objectPool = poolDictionary[tag];
        if (objectPool.Count == 0)
        {
            Debug.LogWarning($"Pool {tag} is empty. Cannot spawn.");
            return null;
        }

        GameObject objectToSpawn = objectPool.Dequeue();
        EnemyBase enemyComponent = objectToSpawn.GetComponent<EnemyBase>();
        if (enemyComponent != null)
        {
            enemyComponent.ResetForPooling();
        }
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.SetActive(true);

        return objectToSpawn;
    }
    public void ReturnToPool(string tag, GameObject objectToReturn)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Destroy(objectToReturn);
            return;
        }
        objectToReturn.SetActive(false);
        poolDictionary[tag].Enqueue(objectToReturn);
    }
}