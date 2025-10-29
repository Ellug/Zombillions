using UnityEngine;

public class SwordMaster : PlayerBase
{
    protected override void Awake()
    {
        base.Awake();
        
        _skills[0] = gameObject.AddComponent<SwordQ>();
        _skills[1] = gameObject.AddComponent<SwordW>();
        _skills[2] = gameObject.AddComponent<SwordE>();
        _skills[3] = gameObject.AddComponent<SwordR>();

        foreach (var skill in _skills)
            skill.Init(this);
    }

    protected override void Attack()
    {
        Debug.Log("검 공격");
    }
}
