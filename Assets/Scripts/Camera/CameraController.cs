using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera _mainCam;
    [SerializeField] private Transform _target;

    [Header("Distance")]
    [SerializeField] private float _distance = 20f;
    [SerializeField] private float _minDis = 10f;
    [SerializeField] private float _maxDis = 40f;

    [Header("Camera Move")]
    [SerializeField] private float _moveSpeed = 40f;
    [SerializeField] private float _edgeSize = 20f;

    [Header("Zoom")]
    [SerializeField] private float _zoomSpeed = 20f;

    private bool _isCameraLock = true;
    private float _camPitch = 50f;
    private Quaternion _fixedRot;

    void Start()
    {
        if (_mainCam == null)
            _mainCam = Camera.main;

        if (_target != null)
            _fixedRot = Quaternion.Euler(_camPitch, 0, 0);
    }

    void Update()
    {
        HandleLockToggle();
        HandleZoom();

        // 락 상태에 따라 가장자리면 이동
        if (_isCameraLock)
            FollowTarget();
        else
            MoveCamera();

        transform.rotation = _fixedRot;
    }

    // 카메라 고정 토글
    private void HandleLockToggle()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            _isCameraLock = !_isCameraLock;
            Cursor.lockState = _isCameraLock ? CursorLockMode.None : CursorLockMode.Confined;
        }
    }

    // 마우스 휠로 줌 인 아웃
    private void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.01f)
        {
            _distance -= scroll * _zoomSpeed;
            _distance = Mathf.Clamp(_distance, _minDis, _maxDis);

            // 자유 시점일 때는 실제 카메라 이동
            if (!_isCameraLock)
            {
                Vector3 camToTarget = (_target != null)
                    ? (transform.position - _target.position)
                    : transform.forward * _distance;

                float curDistance = camToTarget.magnitude;
                float nextDistance = Mathf.Clamp(curDistance - scroll * _zoomSpeed, _minDis, _maxDis);

                float moveDelta = nextDistance - curDistance;
                transform.position += camToTarget.normalized * moveDelta;
            }
        }
    }

    // 타겟 추적
    private void FollowTarget()
    {
        if (_target == null) return;

        Vector3 offset = _fixedRot * new Vector3(0, 0, -_distance);
        Vector3 targetPos = _target.position + offset;

        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 10f);
        // transform.position = targetPos;
    }

    // 카메라 이동
    private void MoveCamera()
    {
        Vector3 dir = Vector3.zero;
        Vector3 camForward = transform.forward;
        Vector3 camRight = transform.right;
        camForward.y = 0;
        camRight.y = 0;

        Vector3 mousePos = Input.mousePosition;
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // 화면 가장자리 감지 및 이동
        if (mousePos.x <= _edgeSize)
            dir -= camRight;
        else if (mousePos.x >= screenWidth - _edgeSize)
            dir += camRight;

        if (mousePos.y <= _edgeSize)
            dir -= camForward;
        else if (mousePos.y >= screenHeight - _edgeSize)
            dir += camForward;

        if (dir != Vector3.zero)
            transform.position += dir.normalized * _moveSpeed * Time.deltaTime;
    }
}
