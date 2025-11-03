using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 화면에 표시할 현재 소지 자원
/// </summary>
public class GoldUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _totalGoldUI;

    [SerializeField] private FloatingText _floatingTextPrefab;

    private GoldManager _goldManager;

    // 골드 값이 변경될 때 호출되는 콜백 함수
    // GoldManager.OnGoldChanged 이벤트에 의해 호출



    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        _goldManager = GetComponent<GoldManager>();
        GameManager.Instance.Gold.OnGoldChanged += HandleGoldChanged;

        if (_totalGoldUI == null)
            _totalGoldUI = GameObject.Find("TotalGold")?.GetComponent<TextMeshProUGUI>();

        HandleGoldChanged(GameManager.Instance.Gold.CurrentGold);
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        GameManager.Instance.Gold.OnGoldChanged -= HandleGoldChanged;
    }

    // 씬 전환 시, "TotalGold" 텍스트를 찾는 메서드
    private void OnSceneLoaded(Scene s, LoadSceneMode m)
    {
        _totalGoldUI = GameObject.Find("TotalGold")?.GetComponent<TextMeshProUGUI>();
    }

    // 소지골드 출력UI
    private void HandleGoldChanged(int value)
    {
        if (_totalGoldUI == null)
            return;

        _totalGoldUI.text = $"{value}";
    }

    // 광석처치 시, 획득 골드 출력UI
    public void GetAddGoldUI(GoldMine goldMine, int reward)
    {
        if (_floatingTextPrefab == null)
        {
            Debug.LogError("_floatingTextPrefab 추가하세요");
            return;
        }
        
        Vector3 screenPos = Camera.main.WorldToScreenPoint(goldMine.transform.position);
        Transform canvasTransform = GameObject.Find("GoldUICanvas").transform;

        FloatingText newText = Instantiate(_floatingTextPrefab, canvasTransform);
        newText.Setup($"+{reward}", Color.red, screenPos);
        

    }

}
