using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GlobalTime Timer { get; private set; }
    public GlobalScore Score { get; private set; }
    public SceneChanger Scene { get; private set; }
    public static GameManager Instance { get; private set; }

    //�̱��� ����
    //1. �ı����� �ʵ��� DonDestroy
    //2. �̹� �ڽ��� �����ϸ� �ı�
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            Timer = GetComponent<GlobalTime>();
            Score = GetComponent<GlobalScore>();
            Scene = GetComponent<SceneChanger>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
