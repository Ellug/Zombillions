using UnityEngine;

public class CharacterSelectUI : MonoBehaviour
{
    [System.Serializable]
    public class CharacterCard
    {
        public string CharacterName;
        public Sprite characterImage;
        public GameObject playerPrefab;
    }

    [Header("Character List")]
    [SerializeField] private CharacterCard[] _characters;

    [Header("UI Reference")]
    [SerializeField] private Transform _cardParent;
    [SerializeField] private GameObject _cardPrefab;

    void Start()
    {
        foreach (var character in _characters)
        {
            GameObject cardObj = Instantiate(_cardPrefab, _cardParent);
            CharacterCardUI cardUI = cardObj.GetComponent<CharacterCardUI>();
            
            cardUI.Init(character.CharacterName, character.characterImage, () =>
            {
                SelectCharacter(character);
            });
        }
    }

    private void SelectCharacter(CharacterCard character)
    {
        GameManager.Instance.SetSelectedCharacter(character.playerPrefab);
        GameManager.Instance.Scene.Load(1);
    }
}
