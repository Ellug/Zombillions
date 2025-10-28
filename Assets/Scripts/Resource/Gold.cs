using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ݱ� ��ü ������Ʈ
/// �ǰݽ� HP ���̰� 0 �� �Ǹ� ���� �����ϰ� �ı�(despawn)
/// �����θ� ���� ������ (GoldSpawner) Ǯ�� �շ�
/// </summary>
public class Gold : MonoBehaviour
{
    [Header("HP / Reward")]
    [Tooltip("�ݱ��� �ִ� ü��")]
    [SerializeField] private float _maxHP = 20f;

    [Tooltip("�ݱ� �ı��� ������ ����")]
    [SerializeField] private int _rewardGold = 10;

    [Header("�浹 / �ǰ� ����")]
    [Tooltip("�´� ������ ���� �ݶ��̴�")]
    [SerializeField] private Collider _hitCollider;

    private float _currentHP;
    private bool _isActive;

    private GoldSpawner _ownerSpawner;

    public void SetOwner(GoldSpawner owner) // ������Ʈ�� ���� ��������� ���
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
            Debug.LogWarning("GoldManager �ν��Ͻ��� ���� ��� ���� ��ŵ");
        }

        // �����ʰ� ������ ����
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
