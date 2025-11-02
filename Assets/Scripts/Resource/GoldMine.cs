using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 금광 본체 컴포넌트
/// 피격시 HP 깎이고 0 이 되면 금을 지급하고 파괴(despawn)
/// 스스로를 만든 스포너 (GoldSpawner) 풀에 합류
/// </summary>
public class GoldMine : MonoBehaviour
{
    [Header("HP / Reward")]
    [Tooltip("금광의 최대 체력")]
    [SerializeField] private float _maxHP = 20f;

    [Tooltip("금광 파괴시 지급할 보상")]
    [SerializeField, Range(1,100)] private int _minReward = 5;
    [SerializeField, Range(1, 100)] private int _maxReward = 15;

    private bool _isActive;
    private float _currentHP;



    void Awake()
    {
        Init();
    }

    public void Init()
    {
        _currentHP = _maxHP;
        _isActive = true;
    }



    public void TakeDamage(float damage)
    {
        if(_isActive == false || damage <= 0f)
            return;

        _currentHP = _currentHP - damage;

        if(_currentHP <= 0f)
            Die();
    }

    private void Die()
    {
        if (_currentHP > 0)
            return;

        _isActive = false;

        int rewardGold = Random.Range(_minReward, _maxReward + 1);
        GameManager.Instance.Gold.AddGold(rewardGold);

        GoldMineSpawner goldMineSpawner = FindAnyObjectByType<GoldMineSpawner>();
        GameManager.Instance.Gold.GoldUI.GetAddGoldUI(this, rewardGold);
        goldMineSpawner.Despawn(this);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.K))
        {
            TakeDamage(1);
        }
    }
}
