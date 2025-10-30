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
        //Canvas�� Ʈ���������� ���Ѵ�.(���� �̹��� ���� ��ġ�� ���ϱ� ���� ���)
        _canvasTransform = GameObject.Find("Canvas").transform;     
        _lightChanger = GetComponent<LIghtChanger>();
    }

    //���ӿ��� �̹��� ���� �޼���
    public void GetGameOver()
    {
        GameManager.Instance.Light.GetLightChange(0f);
        Image _image = Instantiate(Image, _canvasTransform.position, Quaternion.identity, _canvasTransform);
    }
}


