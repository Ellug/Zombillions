using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//"EffectSoundManager" 오브젝트에 인스펙터 추가해서 사용한다.
public class EffectSoundManager : MonoBehaviour
{
    private AudioSource _effectSound;


    void Start()
    {
        _effectSound = GetComponent<AudioSource>();
    }

    public void GetSoundEffect(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogError("효과음 사운드 트랙 추가하세요");
            return;
        }
        _effectSound.PlayOneShot(clip);
    }
}
