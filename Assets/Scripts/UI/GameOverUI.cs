using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField]  private Image _image;

    private Transform _canvasTransform;
    private LIghtChanger _lightChanger;

    void Awake()
    {
        if (_image == null)
        {
            Debug.LogError("GameOver �̹��� ����");
            return;
        }

        //Canvas�� Ʈ���������� ���Ѵ�.(���� �̹��� ���� ��ġ�� ���ϱ� ���� ���)
        _canvasTransform = GameObject.Find("Canvas").transform;     
        _lightChanger = GetComponent<LIghtChanger>();
    }

    //���ӿ��� �̹��� ���� �޼���
    public void GetGameOver()
    {
        GameManager.Instance.Light.GetLightChange(0f);
        //_canvasTransform�� ������Ʈ�� ������� ����
        Image image = Instantiate(_image, _canvasTransform.position, Quaternion.identity, _canvasTransform);
    }
}


