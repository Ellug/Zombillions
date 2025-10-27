using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTime : MonoBehaviour
{
    enum Day
    {
        Noon,Night
    }

    private int gameTime = 0;
    private int gameWave = 1;


    public int GameTime
    {
        get { return gameTime; }
        private set { gameTime = value; }
    }
    public int GameWave
    {
        get { return gameWave; }
        private set { gameWave = value; }
    }

    private void Awake()
    {
        gameTime = 0;
        gameWave = 1;
    }
}
