using UnityEngine;

public class SwordW : SkillBase
{
    void Awake()
    {
        _skillName = "Tornado";
        _coolTime = 6f;
        _icon = Resources.Load<Sprite>("Icons/Skill_SwordW");
    }
    
    protected override void ActivateSkill()
    {
        Debug.Log($"2 스킬 {_skillName} 실행");
    }
}
