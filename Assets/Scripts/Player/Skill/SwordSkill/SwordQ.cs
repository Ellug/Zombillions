using UnityEngine;

public class SwordQ : SkillBase
{
    void Awake()
    {
        _skillName = "Dash";
        _coolTime = 4f;
        _icon = Resources.Load<Sprite>("Icons/Skill_SwordQ");
    }

    protected override void ActivateSkill()
    {
        Debug.Log($"1 스킬 {_skillName} 실행");
    }
}
