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
        // 레이 각 메서드에서 직접 소지 말고 어차피 여러곳에서 사용할 거면 업데이트에서 쏘고 ray만 전달
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

        HandleMouseRotate(ray);
        HandleMouseMove(ray);
        HandleMovementInput();
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
    private void HandleMouseMove(Ray ray)
    {
        if (Input.GetMouseButton(1))
        {            
            if (Physics.Raycast(ray, out RaycastHit hit, 200f, _groundMask))
                _player.SetTargetPosition(hit.point);
        }
    }

    private void HandleMouseRotate(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hit, 200f, _groundMask))
        {
            Vector3 lookPos = hit.point;
            lookPos.y = transform.position.y;

            Vector3 dir = (lookPos - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(dir);
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
