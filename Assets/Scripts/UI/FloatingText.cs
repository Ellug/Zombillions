using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using TMPro;

/// <summary>
/// 위로 천천히 이동하며 사라지는 플로팅 텍스트
/// </summary>
public class FloatingText : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI _text;
    private RectTransform _rect;

    [Header("Motion")]
    [SerializeField] private float _lifeTime = 1.5f;
    [SerializeField] private float _riseSpeed = 40f;
    [SerializeField] private float _fadePerSec = 1.0f;

    private float _elapsed = 0f;

    void Awake()
    {
        _rect = GetComponent<RectTransform>();
        if(_text == null)
        {
            _text = GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    public void Setup(string message, Color color, Vector2 startScreenPos)
    {
        if(_text != null)
        {
            _text.text = message;
            _text.color = color;
        }

        _rect.position = startScreenPos;
    }

    void Update()
    {
        _rect.anchoredPosition += Vector2.up * _riseSpeed * Time.deltaTime;

        if(_text != null)
        {
            Color c = _text.color;
            c.a -= _fadePerSec * Time.deltaTime;
            c.a = Mathf.Clamp01(c.a);
            _text.color = c;
        }

        _elapsed += Time.deltaTime;
        if(_elapsed >= _lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
