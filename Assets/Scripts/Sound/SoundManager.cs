using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//GameManager�� �ν����� �߰��ؼ� ����Ѵ�.
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


    //���̶�Ű���� EffectSoundManager�� ã�� �޼���
    private void OnSceneLoaded(Scene s, LoadSceneMode m)
    {
        if (FindObjectOfType<EffectSoundManager>() == null) 
            return;

        EffectSound = FindAnyObjectByType<EffectSoundManager>();
    }

    //SoundManager�� ����Ʈ������ �����ؼ� BGM ���
    public void GetBGMChage(int index)
    {
        if (_backGroundMugic == null && _backGroundMugic == _soundClip[index]) return;

        if (index >= _soundClip.Count)
        {
            Debug.LogError("BGM���� Ʈ���� ���� �ʰ�");
            return;
        }
        _backGroundMugic.clip = _soundClip[index];
        _backGroundMugic.Play();
    }

    //BGMü���� �� ���̵�ƿ�
    public void GetBGMFadeOut(int index)
    {
        GetBGMChage(index);
    }

    //������ �ٲ�� BGM���̵�ƿ�
    public void OnTimeZoneChange()
    {
        Day timeZone = GameManager.Instance.Timer.CurrentTimeZone;

        if (timeZone == Day.Noon)
        {
            Debug.Log("��� ������ ü����");
            GetBGMFadeOut(1);
            return;
        }
        if (timeZone == Day.Night)
        {
            Debug.Log("��� ������ ü����");
            GetBGMFadeOut(2);
            return;
        }
    }
}
