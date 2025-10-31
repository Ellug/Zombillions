using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool
{
    public string tag;
    public GameObject prefab;
}

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager Instance;
    public List<Pool> pools;
    private Dictionary<string, Queue<GameObject>> poolDictionary;
    private Dictionary<string, Transform> _poolParentDictionary;

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
        _poolParentDictionary = new Dictionary<string, Transform>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            GameObject parent = new GameObject(pool.tag + " Pool Parent");
            parent.transform.SetParent(this.transform);

            _poolParentDictionary.Add(pool.tag, parent.transform);

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
        GameObject objectToSpawn = null;
        if (objectPool.Count == 0)
        {
            Pool poolConfig = pools.Find(p => p.tag == tag);
            if (poolConfig == null)
            {
                Debug.LogError($"[POOL ERROR] Dynamic spawn failed. Pool config not found for tag: {tag}");
                return null;
            }
            Transform parentTransform = _poolParentDictionary[tag];
            objectToSpawn = Instantiate(poolConfig.prefab, parentTransform);
        }
        else
        {
            objectToSpawn = objectPool.Dequeue();
        }

        if (objectToSpawn.transform.parent == null)
        {
            Transform parentTransform = _poolParentDictionary[tag];
            objectToSpawn.transform.SetParent(parentTransform);
        }

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

    public GameObject EnemyHPBarPool (string tag, Transform parent)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogError($"[POOL ERROR] ��û�� UI �±� '{tag}'�� ObjectManager�� ��ϵǾ� ���� �ʽ��ϴ�.");
            return null;
        }

        Queue<GameObject> objectPool = poolDictionary[tag];
        GameObject objectToSpawn = null;

        if (objectPool.Count == 0)
        {
            Pool poolConfig = pools.Find(p => p.tag == tag);
            if (poolConfig == null)
            {
                Debug.LogError($"[POOL ERROR] Dynamic spawn failed. Pool config not found for tag: {tag}");
                return null;
            }
            objectToSpawn = Instantiate(poolConfig.prefab);
        }
        else
        {
            objectToSpawn = objectPool.Dequeue();
        }
        objectToSpawn.transform.SetParent(parent, false);
        objectToSpawn.transform.localPosition = Vector3.zero;
        objectToSpawn.transform.localRotation = Quaternion.identity;
        objectToSpawn.transform.localScale = Vector3.one;

        objectToSpawn.SetActive(true);

        return objectToSpawn;
    }
}