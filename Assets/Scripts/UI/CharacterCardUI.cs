using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCardUI : MonoBehaviour
{
    [SerializeField] private Image _cardImg;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private Button _selectButton;

    public void Init(string name, Sprite img, System.Action onSelect)
    {
        _nameText.text = name;
        _cardImg.sprite = img;

        _selectButton.onClick.RemoveAllListeners();
        _selectButton.onClick.AddListener(() => GameManager.Instance.Sound.GetBGMChage(1));
        _selectButton.onClick.AddListener(() => onSelect?.Invoke());
    }
}
