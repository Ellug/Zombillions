using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTime : MonoBehaviour
{
    [SerializeField] private int _noonTime;
    [SerializeField] private int _nightTime;
    [SerializeField] private float _timeCountSpeed = 1;     //시간올라가는 속도제어

    public Day CurrentTimeZone { get; private set; }
    public int LifeTime { get; private set; }
    public int GameTime { get; private set; }
    public int GameWaveCount { get; private set; }

    //옵저버 패턴
    private List<ITimeObserver> _observer = new();

    public void AddObserver(ITimeObserver observer) => _observer.Add(observer);
    public void RemoveObserver(ITimeObserver observer) => _observer.Remove(observer);

    private void HandleNotifyTimeZoneChange()
    {
        foreach (ITimeObserver observer in _observer)
        {
            observer.OnTimeZoneChange();
        }
    }



    void Awake()
    {
        CurrentTimeZone = Day.Noon;
        GameTime = 0;
        LifeTime = 0;
        GameWaveCount = 1;
    }

    void Start()
    {
        StartCoroutine(TimeCounting());
    }

    //시간 변동 코루틴
    private IEnumerator TimeCounting()
    {
        while (true)
        {
            GameTime++;
            LifeTime++;
            
            GetTimeZoneChange();

            yield return new WaitForSeconds(_timeCountSpeed);
        }
    }


    //GameTime이 기준 시간을 넘어가면 시간대를 변경하는 메서드
    public void GetTimeZoneChange()
    {
        switch (CurrentTimeZone)
        {
            case Day.Noon:

                if (GameTime <= _noonTime)
                    break;
                if (GameTime > _noonTime)
                {
                    CurrentTimeZone = Day.Night;
                    HandleNotifyTimeZoneChange();
                    GameTime = 0;                             //시간대 변경 후에는 GameTime을 초기화
                }
                break;

            case Day.Night:
                if (GameTime <= _nightTime)
                    break;
                if (GameTime > _nightTime)
                {
                    CurrentTimeZone = Day.Noon;
                    HandleNotifyTimeZoneChange();
                    GameTime = 0;                               //시간대 변경 후에는 GameTime을 초기화
                    GameWaveCount++;
                }
                break;
        }
    }
}
public enum Day { Noon, Night }
