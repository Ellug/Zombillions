using UnityEngine;
using TMPro;

/// <summary>
/// 위로 천천히 이동하며 사라지는 플로팅 텍스트
/// </summary>
public class FloatingText : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI _text;
    // 실제로 글자를 표시하는 TextMeshProUGUI
    private RectTransform _rect;
    // 자신의 RectTransform ( UI 위치 / 크기 제어용 )

    [Header("Motion")]
    [SerializeField] private float _lifeTime = 1.5f; // 텍스트가 화면에 존재할 시간
    [SerializeField] private float _riseSpeed = 40f; // 초당 올라가는 속도
    [SerializeField] private float _fadePerSec = 1.0f; // fade 하는 시간 ( 1초에 사라짐 )

    private float _elapsed = 0f; // 경과된 시간

    void Awake()
    {
        _rect = GetComponent<RectTransform>();
        if(_text == null)
        {
            _text = GetComponentInChildren<TextMeshProUGUI>();
        }
    }
    // Awake - 오브젝트 생성 시 가장 먼저 호출
    // RectTransfomr 캐시 : 위치 계산용
    // _text 가 미리 할당되지 않았을 경우 자식 오브젝트에서 자동 탐색

    public void Setup(string message, Color color, Vector2 startScreenPos)
    {
        if(_text != null)
        {
            _text.text = message;
            _text.color = color;
        }

        _rect.position = startScreenPos;
    }
    // 외부에서 이 메서드 호출
    // 텍스트 내용 / 색 / 위치 지정
    // startScreenPos 화면 좌표 기준 시작 위치


    void Update()
    {
        _rect.anchoredPosition += Vector2.up * _riseSpeed * Time.deltaTime;
        // 매 프레임마다 위로 살짝 이동

        if(_text != null)
        {
            Color c = _text.color;
            c.a -= _fadePerSec * Time.deltaTime;
            c.a = Mathf.Clamp01(c.a);
            _text.color = c;
        }
        // _text 의 색상값 가져오기
        // a (알파) 의 투명도를 점점 감소
        // Clamp 는 최소 ~ 최대

        _elapsed += Time.deltaTime;
        if(_elapsed >= _lifeTime)
        {
            Destroy(gameObject);
        }
        // _lifeTime 을 초과하면 gameObject 파괴
    }

    
}
