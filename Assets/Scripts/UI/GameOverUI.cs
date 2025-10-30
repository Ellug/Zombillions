using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] Image Image;

    private Transform _canvasTransform;

    void Awake()
    {
        _canvasTransform = GameObject.Find("Canvas").transform;     //Canvas�� Ʈ���������� ���Ѵ�.(���� �̹��� ���� ��ġ�� ���ϱ� ���� ���)
    }

    public void GetGameOver()
    {
        Image _image = Instantiate(Image, _canvasTransform.position, Quaternion.identity, _canvasTransform);
    }
}


