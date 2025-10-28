using UnityEngine;

public class Gunslinger : PlayerBase
{
    // [SerializeField] private float _atkRange = 20;

    void Awake()
    {
        _skills[0] = gameObject.AddComponent<GunQ>();
        _skills[1] = gameObject.AddComponent<GunW>();
        _skills[2] = gameObject.AddComponent<GunE>();
        _skills[3] = gameObject.AddComponent<GunR>();

        foreach (var skill in _skills)
            skill.Init(this);
    }

    protected override void Attack()
    {
        Debug.Log("공격 : 총알 발사!");
    }
}
