using UnityEngine;

public abstract class SkillBase
{
    protected string _skillName;
    protected float _coolTime;
    protected float _curCool;
    protected int _soundIndex = -1;
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

            // 유효한 사운드 존재시만 재생
            if (_player != null && _player._audioClip != null && _soundIndex >= 0 && _soundIndex < _player._audioClip.Length && _player._audioClip[_soundIndex] != null)
                GameManager.Instance.Sound.EffectSound.GetSoundEffect(_player._audioClip[_soundIndex]);
        }
    }

    protected abstract void ActivateSkill();
}
