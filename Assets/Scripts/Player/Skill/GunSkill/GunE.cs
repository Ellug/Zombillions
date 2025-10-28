using UnityEngine;

public class GunE : SkillBase
{
    void Awake()
    {
        _skillName = "Gravity Bullet";
        _coolTime = 10f;
        _icon = Resources.Load<Sprite>("Icons/Skill_GunE");
    }
    
    protected override void ActivateSkill()
    {
        Debug.Log($"3 스킬 {_skillName} 실행");
    }
}
