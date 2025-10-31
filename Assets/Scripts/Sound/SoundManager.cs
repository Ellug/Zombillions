using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private int _clipElement;
    [SerializeField] private List<AudioClip> _soundClip;

    private AudioSource _backGroundMugic;

    void Start()
    {
        _backGroundMugic = GetComponent<AudioSource>();
        GetBGMChage(_clipElement);
    }

    public void GetBGMChage(int index)
    {
        _backGroundMugic.clip = _soundClip[index];
        _backGroundMugic.Play();
    }

    public void GetBGMChage(int index , int sceneNumber)
    {
        if(index >= _soundClip.Count)
        {
            Debug.LogError("사운드 트랙의 범위 초과");
            return;
        }
        _backGroundMugic.clip = _soundClip[index];
        _backGroundMugic.Play();
    }

    public void GetSoundEffect(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogError("사운드 트랙 추가하세요");
            return;
        }
        AudioSource audioSource = GameManager.Instance.Sound.GetComponent<AudioSource>();
        audioSource.PlayOneShot(clip);
    }
}
