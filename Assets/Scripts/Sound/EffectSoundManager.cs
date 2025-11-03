using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//"EffectSoundManager" 오브젝트에 인스펙터 추가해서 사용한다.
public class EffectSoundManager : MonoBehaviour
{
    private AudioSource _effectSound;
    private int _effectCount;


    void Start()
    {
        if (GetComponent<AudioSource>() == null)
            Debug.LogError("효과음매니저에 AudioSorce 넣어라");
        _effectSound = GetComponent<AudioSource>();
    }

    public void GetSoundEffect(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogError("효과음 사운드 트랙 추가하세요");
            return;
        }
        StartCoroutine(SetNoOverlap(clip));
    }


    private IEnumerator SetNoOverlap(AudioClip clip)
    {
        _effectCount++;
        if(_effectCount <= 5)
        _effectSound.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        _effectCount--;

    }
}
