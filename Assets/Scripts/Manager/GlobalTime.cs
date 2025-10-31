using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTime : MonoBehaviour
{
    [SerializeField] private int _noonTime;
    [SerializeField] private int _nightTime;
    [SerializeField] private float _timeCountSpeed = 1;     //�ð��ö󰡴� �ӵ�����

    public Day CurrentTimeZone { get; private set; }
    public int LifeTime { get; private set; }
    public int GameTime { get; private set; }
    public int GameWaveCount { get; private set; }

    //������ ����
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

    //�ð� ���� �ڷ�ƾ
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


    //GameTime�� ���� �ð��� �Ѿ�� �ð��븦 �����ϴ� �޼���
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
                    GameTime = 0;                             //�ð��� ���� �Ŀ��� GameTime�� �ʱ�ȭ
                }
                break;

            case Day.Night:
                if (GameTime <= _nightTime)
                    break;
                if (GameTime > _nightTime)
                {
                    CurrentTimeZone = Day.Noon;
                    HandleNotifyTimeZoneChange();
                    GameTime = 0;                               //�ð��� ���� �Ŀ��� GameTime�� �ʱ�ȭ
                    GameWaveCount++;
                }
                break;
        }
    }
}
public enum Day { Noon, Night }
