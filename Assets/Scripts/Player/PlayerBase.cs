using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBase : MonoBehaviour
{
    [SerializeField] protected int _maxHP = 100;
    [SerializeField] protected int _curHP = 100;
    [SerializeField] protected float _moveSpeed = 20;
    [SerializeField] protected float _rotSpeed = 100;
    [SerializeField] protected float _atkDelay = 100;
    [SerializeField] protected int _def = 100;
    [SerializeField] protected int _atk = 100;

    [SerializeField] private bool _isPlayerAlive = true;
    // [SerializeField] private float _reviveTime = 20;
    // [SerializeField] private float _reviveTimer = 20;

    [SerializeField] private GameObject _player;
    [SerializeField] private LayerMask _groundMask;

    private Camera _mainCam;
    private bool _hasTarget;
    private Vector3 _targetPos;

    protected virtual void Awake()
    {
        if (_mainCam == null)
            _mainCam = Camera.main;
    }

    protected virtual void Update()
    {
        if (!_isPlayerAlive) return;

        HandleMouseInput();
        MoveToTarget();
        MoveToKey();
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButton(1))
        {
            Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, _groundMask))
            {
                _targetPos = hit.point;
                _hasTarget = true;
            }
        }
    }

    private void MoveToTarget()
    {
        if (!_hasTarget) return;

        Vector3 dir = _targetPos - transform.position;
        dir.y = 0;
        float distance = dir.sqrMagnitude;

        if (distance > 0.05f)
        {
            Quaternion targetRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, _rotSpeed * Time.deltaTime);

            Vector3 move = dir.normalized * _moveSpeed * Time.deltaTime;
            if (move.sqrMagnitude > distance) move = dir.normalized * distance;

            transform.position += move;
        }
        else
        {
            _hasTarget = false;
        }
    }

    protected virtual void MoveToKey()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (h == 0 && v == 0) return;

        Vector3 forward = _mainCam.transform.forward;
        Vector3 right = _mainCam.transform.right;
        forward.y = 0f;
        right.y = 0f;

        Vector3 moveDir = (forward * v + right * h).normalized;
        transform.position += moveDir * _moveSpeed * Time.deltaTime;

        transform.rotation = Quaternion.LookRotation(moveDir);
    }

    protected virtual void TakeDamage(int dmg)
    {
        int finalDmg = dmg - _def;

        if (finalDmg > 0)
            _curHP -= finalDmg;
        else
            _curHP--;

        // 데미지 계산 후 _curHP 0 이하면 사망처리
        if (_curHP <= 0 && _isPlayerAlive)
            Die();
    }

    protected virtual void Die()
    {
        if (!_isPlayerAlive) return;
        _isPlayerAlive = false;
        // 이후 카운트 관련 로직 등 예정
    }

    protected abstract void Attack();
}
