using UnityEngine;

public abstract class SkillBase : MonoBehaviour
{
    protected string _skillName;
    protected float _coolTime;
    protected float _curCool;
    protected Sprite _icon;

    public Sprite Icon { get { return _icon; }}

    public float CoolTime { get { return _coolTime; }}
    public float CurCool { get { return _curCool; }}

    protected PlayerBase _player;

    public virtual void Init(PlayerBase player)
    {
        _player = player;
        _curCool = 0f;
    }

    public virtual void UpdateSkillCool(float delta)
    {
        if (_curCool > 0f)
            _curCool -= delta;
    }

    public void TryUse()
    {
        if (_curCool <= 0f)
        {
            ActivateSkill();
            _curCool = _coolTime;
        }
    }

    protected abstract void ActivateSkill();
}
