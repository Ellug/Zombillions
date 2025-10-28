using UnityEngine;

public class GunQ : SkillBase
{
    void Awake()
    {
        _skillName = "Shot Gun";
        _coolTime = 4f;
        _icon = Resources.Load<Sprite>("Icons/Skill_GunQ");
    }

    protected override void ActivateSkill()
    {
        Debug.Log($"1 스킬 {_skillName} 실행");
    }
}
