using System;
using UnityEngine;

public abstract class PlayerBase : MonoBehaviour
{
    [Header("Status")]
    [SerializeField] protected float _maxHp = 100f;
    [SerializeField] protected float _curHp = 100f;
    [SerializeField] protected float _moveSpeed = 20f;
    [SerializeField] protected float _atkDelay = 0.4f;
    [SerializeField] protected float _def = 0f;
    [SerializeField] protected float _atk = 10f;

    [Header("System")]
    [SerializeField] private bool _isPlayerAlive = true;
    [SerializeField] private float _reviveTime = 20f;
    [SerializeField] private float _reviveTimer = 20f;

    [SerializeField] private GameObject _player;

    // UseSkill()에 효과음 출력 메서드 삽입
    // TryAttack()에 효과음 출력 메서드 삽입 (인덱스는 4번의 음악을 사용)
    public AudioClip[] _audioClip;


    // property
    public float CurHp { get { return _curHp; } }
    public float MaxHp { get { return _maxHp; } }
    public float Atk { get { return _atk; } }
    
    private float _atkTimer = 0f;

    private Rigidbody _rb;
    private Vector3 _targetPos;
    private Vector2 _moveInput;
    public BulletSpawner _bulletSpawner;
    private Transform _spawnPoint;

    
    public void MoveInput(Vector2 input) => _moveInput = input;
    public void SetTargetPosition(Vector3 pos) => _targetPos = pos;
    protected SkillBase[] _skills = new SkillBase[4];

    // Skill Getter
    public SkillBase GetSkill(int index)
    {
        if (index < 0 || index >= _skills.Length) return null;
        return _skills[index];
    }

    // Spawn Point Setter
    public void SetSpawnPoint(Transform spawn)
    {
        _spawnPoint = spawn;
    }

    protected virtual void Awake()
    {
        _bulletSpawner = FindObjectOfType<BulletSpawner>();
        if (_bulletSpawner == null) Debug.LogError("[PlayerBase] No BulletSpawner in scene.");
    }
    

    void Start()
    {
        if (_rb == null) _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        UpdateCoolTime();

        if (_isPlayerAlive)
        {
            MoveToTarget();
            MoveByInput();
        }
        else
        {
            UpdateRevive();
        }
    }

    // 해당 지점으로 이동
    private void MoveToTarget()
    {
        if (_targetPos == Vector3.zero) return;
        // 키 입력시 마우스 이동 취소 (합산 방지)
        if (_moveInput != Vector2.zero)
        {
            _targetPos = Vector3.zero;
            return;
        }

        Vector3 dir = _targetPos - transform.position;
        dir.y = 0;
        float distance = dir.sqrMagnitude;

        if (distance > 0.05f)
        {
            Vector3 move = dir.normalized * _moveSpeed * Time.deltaTime;
            if (move.sqrMagnitude > distance) move = dir.normalized * distance;

            transform.position += move;
        }
        else
        {
            _targetPos = Vector3.zero;
            return;
        }
    }

    // 키 인풋을 통한 이동
    private void MoveByInput()
    {
        if (_moveInput == Vector2.zero) return;

        _targetPos = Vector3.zero;
        Vector3 moveDir = new Vector3(_moveInput.x, 0, _moveInput.y).normalized;
        transform.position += moveDir * _moveSpeed * Time.deltaTime;
    }

    // 대미지 처리
    public void TakeDamage(float dmg)
    {
        float finalDmg = dmg - _def;

        if (finalDmg > 0)
            _curHp -= finalDmg;
        else
            _curHp--;

        // 데미지 계산 후 _curHP 0 이하면 사망처리
        if (_curHp <= 0 && _isPlayerAlive)
            Die();
    }

    // 사망 처리
    private void Die()
    {
        if (!_isPlayerAlive) return;
        _isPlayerAlive = false;

        _moveInput = Vector2.zero;
        _targetPos = Vector3.zero;

        // 파괴 및 비활성화 X, 물리 시각 처리만 off
        _rb.isKinematic = true;
        _rb.useGravity = false;

        foreach (var ren in GetComponentsInChildren<Renderer>())
            ren.enabled = false;
        foreach (var col in GetComponentsInChildren<Collider>())
            col.enabled = false;

        Debug.Log("사망");
    }

    // 사망 카운터 작동. 카운터 0되면 부활
    private void UpdateRevive()
    {
        _reviveTimer -= Time.deltaTime;

        // 부활
        if (_reviveTimer <= 0)
        {
            _isPlayerAlive = true;
            _reviveTimer = _reviveTime;

            _moveInput = Vector2.zero;
            _targetPos = Vector3.zero;

            // 부활 위치로 이동 및 Hp 초기화
            transform.position = _spawnPoint.position;
            _curHp = _maxHp;

            // 물리, 시각 처리 on
            _rb.isKinematic = false;
            _rb.useGravity = true;

            foreach (var ren in GetComponentsInChildren<Renderer>())
                ren.enabled = true;
            foreach (var col in GetComponentsInChildren<Collider>())
                col.enabled = true;
        }
    }

    // 공격, 스킬 쿨타임 업데이트
    private void UpdateCoolTime()
    {
        // 어택 쿨타임
        if (_atkTimer > 0f)
            _atkTimer -= Time.deltaTime;

        // 전체 스킬 쿨타임 감소
        foreach (var skill in _skills)
        {
            if (skill != null)
                skill.UpdateSkillCool(Time.deltaTime);
        }
    }

    // 공격
    public void TryAttack()
    {
        if (_atkTimer <= 0f && _isPlayerAlive)
        {
            Attack();
            _atkTimer = _atkDelay;

            if (_audioClip.Length >= 4)
            GameManager.Instance.Sound.EffectSound.GetSoundEffect(_audioClip[4]);
        }
    }

    protected abstract void Attack();

    // 스킬 사용
    public void UseSkill(int index)
    {
        if (index < 0 || index >= _skills.Length || !_isPlayerAlive) return;
        _skills[index]?.TryUse();
    }

    void LateUpdate()
    {
        if (_rb != null)
        {
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
        }
    }
}
