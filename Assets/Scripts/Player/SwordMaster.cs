using UnityEngine;

public class SwordMaster : PlayerBase
{
    protected override void Awake()
    {
        base.Awake();
        
        _skills[0] = new SwordQ();
        _skills[1] = new SwordQ();
        _skills[2] = new SwordQ();
        _skills[3] = new SwordQ();

        foreach (var skill in _skills)
            skill.Init(this);
    }

    protected override void Attack()
    {
        Debug.Log("검 공격");
    }
}
