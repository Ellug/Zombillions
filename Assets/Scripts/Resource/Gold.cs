using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 금광 본체 컴포넌트
/// 피격시 HP 깎이고 0 이 되면 금을 지급하고 파괴(despawn)
/// 스스로를 만든 스포너 (GoldSpawner) 풀에 합류
/// </summary>
public class Gold : MonoBehaviour
{
    [Header("HP / Reward")]
    [Tooltip("금광의 최대 체력")]
    [SerializeField] private float _maxHP = 20f;

    [Tooltip("금광 파괴시 지급할 보상")]
    [SerializeField] private int _rewardGold = 10;

    [Header("충돌 / 피격 판정")]
    [Tooltip("맞는 판정을 받을 콜라이더")]
    [SerializeField] private Collider _hitCollider;

    private float _currentHP;
    private bool _isActive;

    private GoldSpawner _ownerSpawner;

    public void SetOwner(GoldSpawner owner) // 오브젝트를 누가 만들었는지 기억
    {
        _ownerSpawner = owner;
    }

    void Awake()
    {
        if(_hitCollider == null)
        {
            _hitCollider = GetComponent<Collider>();
        }
    }

    private void OnEnable()
    {
        _currentHP = _maxHP;
        _isActive = true;

        if(_hitCollider != null)
        {
            _hitCollider.enabled = true;
        }
    }

    private void OnDisable()
    {
        
    }

    public void TakeDamage(float damage)
    {
        if(_isActive == false)
        {
            return;
        }

        if(damage <= 0f)
        {
            return;
        }

        _currentHP = _currentHP - damage;

        if(_currentHP <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        if(_isActive == false)
        {
            return;
        }

        _isActive = false;

        if(_hitCollider != null)
        {
            _hitCollider.enabled = false;
        }

        if(GoldManager.HasInstance)
        {
            GoldManager.Instance.AddGold(_rewardGold);
        }
        else
        {
            Debug.LogWarning("GoldManager 인스턴스가 없어 골드 지급 스킵");
        }

        // 스포너가 있으면 디스폰
        if (_ownerSpawner != null)
        {
            _ownerSpawner.Despawn(this);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
