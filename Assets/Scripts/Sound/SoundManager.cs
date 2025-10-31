using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//GameManager에 인스펙터 추가해서 사용한다.
public class SoundManager : MonoBehaviour
{
    [SerializeField] private int _clipElement;
    [SerializeField] private List<AudioClip> _soundClip;

    public EffectSoundManager EffectSound {  get; private set; }
    private AudioSource _backGroundMugic;
    


    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void Start()
    {
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
}
