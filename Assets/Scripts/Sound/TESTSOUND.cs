using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTSOUND : MonoBehaviour
{
    public AudioClip AudioClip;


    private void Update()
    {
        GameManager.Instance.Sound.GetSoundEffect(AudioClip);
    }

}
