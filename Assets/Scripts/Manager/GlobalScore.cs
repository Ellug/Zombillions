using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScore : MonoBehaviour
{
    public int GameScore { get; private set; }

    private List<IObserver> _observer = new List<IObserver>();

    public void AddObserver(IObserver observer) => _observer.Add (observer);
    public void RemoveObserver(IObserver observer) => _observer.Remove (observer);

    void Start()
    {
        Notify();
    }

    private void Notify()
    {
        foreach (IObserver score in _observer)
        {
            score.OnNotify();
        }
    }


    public int SumScore()
    {
        int finalTime = GameManager.Instance.Timer.GameTime;
        int finalWave = GameManager.Instance.Timer.GameWave;

        return GameScore += finalTime + finalWave;
    }
}
