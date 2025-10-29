using UnityEngine;

[RequireComponent(typeof(PlayerBase))]
public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private LayerMask _groundMask;
    
    private PlayerBase _player;
    private Camera _cam;

    private void Awake()
    {
        _player = GetComponent<PlayerBase>();
        _cam = Camera.main;
    }

    private void Update()
    {
        HandleMouseRotate();
        HandleMovementInput();
        HandleMouseMove();
        HandleMouseLeftInput();
        HandleAttackInput();
        HandleSkillInput();
    }

    // 방향키 입력
    private void HandleMovementInput()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        _player.MoveInput(new Vector2(h, v));
    }

    // 마우스 입력
    private void HandleMouseMove()
    {
        if (Input.GetMouseButton(1))
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f, _groundMask))
                _player.SetTargetPosition(hit.point);
        }
    }

    private void HandleMouseRotate()
    {
        // 커서 위치에서 레이캐스트
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, _groundMask))
        {
            Vector3 lookPos = hit.point;
            lookPos.y = transform.position.y;

            Vector3 dir = (lookPos - transform.position).normalized;
            if (dir.sqrMagnitude > 0.01f)
            {
                Quaternion targetRot = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    targetRot,
                    Time.deltaTime * 25f // 회전 속도 조정
                );
            }
        }
    }

    // 어택 (스페이스) 입력
    private void HandleAttackInput()
    {
        if (Input.GetKey(KeyCode.Space))
            _player.TryAttack();
    }

    // 어택 (좌클릭) 입력
    private void HandleMouseLeftInput()
    {
        if (Input.GetMouseButton(0))
            _player.TryAttack();
    }
    
    // 스킬 입력 1 2 3 4
    private void HandleSkillInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) _player.UseSkill(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) _player.UseSkill(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) _player.UseSkill(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) _player.UseSkill(3);
    }
}
