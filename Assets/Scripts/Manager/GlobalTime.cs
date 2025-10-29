using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTime : MonoBehaviour
{
    [SerializeField] private int _noonTime;
    [SerializeField] private int _nightTime;

    public Day CurrentTimeZone { get; private set; }
    public int GameTime { get; private set; }
    public int GameWave { get; private set; }

    //옵저버 패턴
    private List<ITimeObserver> _observer = new();

    public void AddObserver(ITimeObserver observer) => _observer.Add(observer);
    public void RemoveObserver(ITimeObserver observer) => _observer.Remove(observer);

    private void Notify()
    {
        foreach (ITimeObserver observer in _observer)
        {
            observer.OnNotify();
        }
    }



    void Awake()
    {
        GameTime = 0;
        GameWave = 1;
    }

    void Start()
    {
        StartCoroutine(TimeCounting());
        Notify();
    }


    private IEnumerator TimeCounting()
    {
        while (true)
        {
            GameTime++;
            Debug.Log($"게임시간: {GameTime}");
            TimeZoneChange();
            yield return new WaitForSeconds(1f);
        }
    }


    //GameTime이 기준 시간을 넘어가면 시간대를 변경하는 메서드
    public void TimeZoneChange()
    {
        switch (CurrentTimeZone)
        {
            case Day.Noon:
                if (GameTime >= _noonTime)
                    CurrentTimeZone = Day.Night;
                break;

            case Day.Night:
                if (GameTime >= _nightTime)
                    CurrentTimeZone = Day.Noon;
                break;
        }
        GameTime = 0;                               //시간대 변경 후에는 GameTime을 초기화
        Debug.Log($"시간대: {CurrentTimeZone}");

    }

}

public enum Day { Noon, Night }