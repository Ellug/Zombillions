using UnityEngine;

public class SwordE : SkillBase
{
    void Awake()
    {
        _skillName = "Burn Brain Bong";
        _coolTime = 10f;
        _icon = Resources.Load<Sprite>("Icons/Skill_SwordE");
    }
    
    protected override void ActivateSkill()
    {
        Debug.Log($"3 스킬 {_skillName} 실행");
    }
}
