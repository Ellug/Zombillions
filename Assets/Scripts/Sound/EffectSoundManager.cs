using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//"EffectSoundManager" ������Ʈ�� �ν����� �߰��ؼ� ����Ѵ�.
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
            Debug.LogError("ȿ���� ���� Ʈ�� �߰��ϼ���");
            return;
        }
        _effectSound.PlayOneShot(clip);
    }
}
