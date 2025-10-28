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
        HandleMovementInput();
        HandleMouseMove();
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

    // 어택 (스페이스) 입력
    private void HandleAttackInput()
    {
        if (Input.GetKey(KeyCode.Space))
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
