using UnityEngine;

public class SwordR : SkillBase
{
    void Awake()
    {
        _skillName = "GiglaDrillBreak";
        _coolTime = 20f;
        _icon = Resources.Load<Sprite>("Icons/Skill_GunR");
    }
    
    protected override void ActivateSkill()
    {
        Debug.Log($"4 스킬 {_skillName} 실행");
    }
}
