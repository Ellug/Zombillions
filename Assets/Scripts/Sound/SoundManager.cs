using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//GameManager�� �ν����� �߰��ؼ� ����Ѵ�.
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
}
