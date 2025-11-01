using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//GameManager에 인스펙터 추가해서 사용한다.
public class SoundManager : MonoBehaviour , ITimeObserver
{
    [SerializeField] private int _clipElement;
    [SerializeField] private List<AudioClip> _soundClip;

    public EffectSoundManager EffectSound {  get; private set; }
    private AudioSource _backGroundMugic;

    private GlobalTime _globalTime;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void Start()
    {
        _globalTime = GameManager.Instance.Timer;
        _globalTime.AddObserver(this);
        _backGroundMugic = GetComponent<AudioSource>();
        GetBGMChage(_clipElement);
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    //하이라키에서 EffectSoundManager를 찾는 메서드
    private void OnSceneLoaded(Scene s, LoadSceneMode m)
    {
        if (FindObjectOfType<EffectSoundManager>() == null) 
            return;

        EffectSound = FindAnyObjectByType<EffectSoundManager>();
    }

    //SoundManager의 사운드트랙에서 선택해서 BGM 출력
    public void GetBGMChage(int index)
    {
        if (_backGroundMugic == null && _backGroundMugic == _soundClip[index]) return;

        if (index >= _soundClip.Count)
        {
            Debug.LogError("BGM사운드 트랙의 범위 초과");
            return;
        }
        _backGroundMugic.clip = _soundClip[index];
        _backGroundMugic.Play();
    }

    //BGM체인지 때 페이드아웃
    public void GetBGMFadeOut(int index)
    {
        GetBGMChage(index);
    }

    //낮밤이 바뀌면 BGM페이드아웃
    public void OnTimeZoneChange()
    {
        Day timeZone = GameManager.Instance.Timer.CurrentTimeZone;

        if (timeZone == Day.Noon)
        {
            Debug.Log("브금 낮으로 체인지");
            GetBGMFadeOut(1);
            return;
        }
        if (timeZone == Day.Night)
        {
            Debug.Log("브금 밤으로 체인지");
            GetBGMFadeOut(2);
            return;
        }
    }
}
