using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] Image Image;

    private Transform _canvasTransform;
    private LIghtChanger _lightChanger;

    void Awake()
    {
        //Canvas의 트렌스폼값을 구한다.(하위 이미지 생성 위치를 정하기 위해 사용)
        _canvasTransform = GameObject.Find("Canvas").transform;     
        _lightChanger = GetComponent<LIghtChanger>();
    }

    //게임오버 이미지 생성 메서드
    public void GetGameOver()
    {
        GameManager.Instance.Light.GetLightChange(0f);
        Image _image = Instantiate(Image, _canvasTransform.position, Quaternion.identity, _canvasTransform);
    }
}


