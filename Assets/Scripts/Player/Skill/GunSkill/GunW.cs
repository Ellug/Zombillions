using UnityEngine;

public class GunW : SkillBase
{
    void Awake()
    {
        _skillName = "Sniper Rifle";
        _coolTime = 6f;
        _icon = Resources.Load<Sprite>("Icons/Skill_GunW");
    }
    
    protected override void ActivateSkill()
    {
        Debug.Log($"2 스킬 {_skillName} 실행");
    }
}
