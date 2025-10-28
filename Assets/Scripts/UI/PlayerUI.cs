using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Image _hpBarFill;
    [SerializeField] private PlayerBase _player;

    private float _blinkSpeed = 5f;

    void Update()
    {
        if (_player == null) return;

        float fill = Mathf.Clamp01(_player.CurHp / _player.MaxHp);
        _hpBarFill.fillAmount = fill;

        // fill bar color
        if (fill > 0.6f)
        {
            _hpBarFill.color = Color.green;
        }
        else if (fill > 0.3f)
        {
            _hpBarFill.color = Color.yellow;
        }
        else
        {
            // sin 파형 깜빡임 추가
            float blink = (Mathf.Sin(Time.time * _blinkSpeed) + 1f) / 2f;
            _hpBarFill.color = Color.Lerp(Color.red * 0.2f, Color.red, blink);
        }
    }

    public void SetPlayer(PlayerBase player)
    {
        _player = player;
    }
}