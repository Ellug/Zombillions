using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTime : MonoBehaviour
{
    [SerializeField] private int _dayChangeTime;
    public int GameTime {  get; private set; }
    public int GameWave {  get; private set; }
    
    

    private void Awake()
    {
        GameTime = 0;
        GameWave = 1;
    }

    
}

public enum Day { Noon, Night }