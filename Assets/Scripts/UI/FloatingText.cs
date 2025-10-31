using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using TMPro;

/// <summary>
/// ���� õõ�� �̵��ϸ� ������� �÷��� �ؽ�Ʈ
/// </summary>
public class FloatingText : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI _text;
    // ������ ���ڸ� ǥ���ϴ� TextMeshProUGUI
    private RectTransform _rect;
    // �ڽ��� RectTransform ( UI ��ġ / ũ�� ����� )

    [Header("Motion")]
    [SerializeField] private float _lifeTime = 1.5f; // �ؽ�Ʈ�� ȭ�鿡 ������ �ð�
    [SerializeField] private float _riseSpeed = 40f; // �ʴ� �ö󰡴� �ӵ�
    [SerializeField] private float _fadePerSec = 1.0f; // fade �ϴ� �ð� ( 1�ʿ� ����� )

    private float _elapsed = 0f; // ����� �ð�

    void Awake()
    {
        _rect = GetComponent<RectTransform>();
        if(_text == null)
        {
            _text = GetComponentInChildren<TextMeshProUGUI>();
        }
    }
    // Awake - ������Ʈ ���� �� ���� ���� ȣ��
    // RectTransfomr ĳ�� : ��ġ ����
    // _text �� �̸� �Ҵ���� �ʾ��� ��� �ڽ� ������Ʈ���� �ڵ� Ž��

    public void Setup(string message, Color color, Vector2 startScreenPos)
    {
        if(_text != null)
        {
            _text.text = message;
            _text.color = color;
        }

        _rect.position = startScreenPos;
    }
    // �ܺο��� �� �޼��� ȣ��
    // �ؽ�Ʈ ���� / �� / ��ġ ����
    // startScreenPos ȭ�� ��ǥ ���� ���� ��ġ


    void Update()
    {
        _rect.anchoredPosition += Vector2.up * _riseSpeed * Time.deltaTime;
        // �� �����Ӹ��� ���� ��¦ �̵�

        if(_text != null)
        {
            Color c = _text.color;
            c.a -= _fadePerSec * Time.deltaTime;
            c.a = Mathf.Clamp01(c.a);
            _text.color = c;
        }
        // _text �� ���� ��������
        // a (����) �� ������ ���� ����
        // Clamp �� �ּ� ~ �ִ�

        _elapsed += Time.deltaTime;
        if(_elapsed >= _lifeTime)
        {
            Destroy(gameObject);
        }
        // _lifeTime �� �ʰ��ϸ� gameObject �ı�
    }

    
}
