using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GlobalTime Timer { get; private set; }
    public GlobalScore Score { get; private set; }
    public SceneChanger Scene { get; private set; }
    public static GameManager Instance { get; private set; }

    //싱글톤 패턴
    //1. 파괴되지 않도록 DonDestroy
    //2. 이미 자신이 존재하면 파괴
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
