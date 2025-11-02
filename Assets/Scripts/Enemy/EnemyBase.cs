using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] public float _EnemyMAXHP = 10f;
    [SerializeField] public float _EnemyHP = 10f;
    [SerializeField] public float _EnemyMoveSpeed = 10f;
    [SerializeField] public float _EnemyDMG = 5f;
    [SerializeField] public float _ViewRange = 5f;
    public Transform _targetTransform;
    public SphereCollider _sphereCollider;
    [SerializeField] public float _AttackDelay;
    [SerializeField] public string _poolTag = "Enemy";
    public bool _Chase = false;
    private EnemyHPBar _myHealthBar;
    private static Transform _mainCanvasTransform;

    // EnemyAttack클래스의 ApplyDamage() 메서드에서 플레이어 공격시에만 사운드 추가
    public AudioClip _enemyAttackSound;
    // EnemyBase클래스의 Die()에 죽는 사운드 추가
    public AudioClip _enemyDieSound;

    protected virtual void Start()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        _sphereCollider.radius = _ViewRange;
    }

    protected virtual void Update()
    {
        if (_Chase == true)
        {
            if (_targetTransform != null)
            {
                Vector3 positionY0 = _targetTransform.position;
                positionY0.y = transform.position.y;
                _targetTransform.position = positionY0;
                transform.LookAt(_targetTransform.position);
                transform.position = Vector3.MoveTowards(transform.position, _targetTransform.position, _EnemyMoveSpeed * Time.deltaTime);
            }
        }
    }  
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tower"))
        {
            // Debug.Log("Tower OnTriggerEnter");
            _targetTransform = other.transform;
            _Chase = true;
        }
        else if (other.CompareTag("Player"))
        {
            // Debug.Log("Player OnTriggerEnter");
            _targetTransform = other.transform;
            _Chase = true;
        }
        else if (other.CompareTag("HQ")) 
        {
            // Debug.Log("HQ OnTriggerEnter");
            _targetTransform = other.transform;
            _Chase = true;
        }
    }

    public virtual void ResetForPooling()
    {
        if (_mainCanvasTransform == null)
        {
            GameObject mainCanvas = GameObject.FindWithTag("EnemyHPBar");
            if (mainCanvas != null)
            {
                _mainCanvasTransform = mainCanvas.transform;
            }
            else
            {
                Debug.LogError("씬에서 메인 Canvas 오브젝트를 찾을 수 없습니다!");
            }
        }

        _Chase = false;
        _targetTransform = null;
        _EnemyHP = _EnemyMAXHP;
        _targetTransform = null;

        if (_mainCanvasTransform != null)
        {
            GameObject hpBarGO = ObjectManager.Instance.EnemyHPBarPool("HPBar", _mainCanvasTransform);
            if (hpBarGO != null)
            {
                _myHealthBar = hpBarGO.GetComponent<EnemyHPBar>();
                _myHealthBar.Setup(this);
            }
            if (hpBarGO != null)
            {
                _myHealthBar = hpBarGO.GetComponent<EnemyHPBar>();
                _myHealthBar.Setup(this);

                // if (_myHealthBar.TargetEnemyBase == this)
                // {
                //     Debug.Log($"HP Bar 할당 성공: {gameObject.name} -> {_myHealthBar.gameObject.name}");
                // }
                // else
                // {
                //     Debug.LogError("HP Bar 할당 실패! TargetEnemyBase가 null이거나 다릅니다.");
                // }
            }
        }
    }

    public virtual void TakeDamage(float dmg) 
    {
        if (dmg > 0)
            _EnemyHP -= dmg;
        else
            _EnemyHP--;

        if (_EnemyHP <= 0)
            Die();
        if (_myHealthBar != null && !_myHealthBar.gameObject.activeSelf)
        {
            _myHealthBar.gameObject.SetActive(true);
        }
    }

    public virtual void Die() 
    {
        if (ObjectManager.Instance != null && !string.IsNullOrEmpty(_poolTag))
        {
            ObjectManager.Instance.ReturnToPool(_poolTag, gameObject);
            if (_myHealthBar != null)
            {
                // 에너미 플레이어 죽을 때, 효과음
                if(_enemyDieSound != null)
                    GameManager.Instance.Sound.EffectSound.GetSoundEffect(_enemyDieSound);

                ObjectManager.Instance.ReturnToPool("HPBar", _myHealthBar.gameObject);
                _myHealthBar = null;

            }
        }
        else
        {
            Debug.Log("몬스터 사망처리 에러");
        }
    }
}
