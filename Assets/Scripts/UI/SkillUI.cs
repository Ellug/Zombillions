using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    [System.Serializable]
    public class SkillSlot
    {
        public Image icon;
        public Image overay;
        public TMP_Text coolText;
    }

    [SerializeField] private SkillSlot[] _slots = new SkillSlot[4];
    [SerializeField] private PlayerBase _player;

    void Update()
    {
        if (_player == null) return;

        for (int i = 0; i < _slots.Length; i++)
        {
            SkillBase skill = GetSkill(i);
            if (skill == null) continue;

            float cur = skill.CurCool;
            float max = skill.CoolTime;

            float fill = Mathf.Clamp01(cur / max);
            _slots[i].overay.fillAmount = fill;

            if (cur > 0f)
            {
                _slots[i].coolText.text = Mathf.Ceil(cur).ToString();
            }
            else
            {
                _slots[i].coolText.text = "";
            }
        }
    }

    public void SetPlayer(PlayerBase player)
    {
        _player = player;

        // 스킬 아이콘 세팅
        for (int i = 0; i < _slots.Length; i++)
        {
            SkillBase skill = GetSkill(i);

            _slots[i].icon.sprite = skill.Icon;
            _slots[i].icon.enabled = true;
        }
    }
    
    public SkillBase GetSkill(int index)
    {
        return _player.GetSkill(index);
    }
}
