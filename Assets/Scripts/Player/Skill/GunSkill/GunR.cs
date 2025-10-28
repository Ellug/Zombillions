using UnityEngine;

public class GunR : SkillBase
{
    void Awake()
    {
        _skillName = "Gattling Gun";
        _coolTime = 20f;
        _icon = Resources.Load<Sprite>("Icons/Skill_GunR");
    }
    
    protected override void ActivateSkill()
    {
        Debug.Log($"4 스킬 {_skillName} 실행");
    }
}
