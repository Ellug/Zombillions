using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera _mainCam;
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _playArea;

    [Header("Distance")]
    [SerializeField] private float _distance = 20f;
    [SerializeField] private float _minDis = 10f;
    [SerializeField] private float _maxDis = 60f;

    [Header("Camera Move")]
    [SerializeField] private float _moveSpeed = 50f;
    [SerializeField] private float _edgeSize = 20f;

    [Header("Zoom")]
    [SerializeField] private float _zoomSpeed = 30f;

    private bool _isCameraLock = true;
    private float _camPitch = 50f;
    private Quaternion _fixedRot;

    private Vector3 _minBounds;
    private Vector3 _maxBounds;
    private Renderer render;

    void Start()
    {
        if (_mainCam == null)
            _mainCam = Camera.main;

        if (_target != null)
            _fixedRot = Quaternion.Euler(_camPitch, 0, 0);

        if (_playArea != null)
        {
            render = _playArea.GetComponent<Renderer>();
            if (render != null)
            {
                Bounds b = render.bounds;
                _minBounds = b.min;
                _maxBounds = b.max;
            }
        }
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
                // y축만 계산                
                float curYDist = Mathf.Abs(transform.position.y - _target.position.y);
                float nextYDist = Mathf.Clamp(curYDist - scroll * _zoomSpeed, _minDis, _maxDis);
                float moveDelta = nextYDist - curYDist;

                Vector3 moveDir = Quaternion.Euler(_camPitch, 0, 0) * Vector3.back;
                transform.position += moveDir.normalized * moveDelta;
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
        {
            // 줌 비례 속도 보정 (멀리서 더 빨리 이동)
            // 비율 산출
            float zoomFactor = Mathf.InverseLerp(_minDis, _maxDis, _distance);
            // 비율에 따라 계산
            float finalSpeed = _moveSpeed * Mathf.Lerp(0.6f, 2.0f, zoomFactor);
            transform.position += dir.normalized * finalSpeed * Time.deltaTime;
        }

        ClampPosition();
    }

    // 카메라 영역 제한 -> fix 필요
    private void ClampPosition()
    {
        if (_playArea == null) return;

        Vector3 pos = transform.position;

        // Camera 시야 폭/높이 계산
        float pitchRad = _camPitch * Mathf.Deg2Rad;
        float forwardOffset = _distance * Mathf.Cos(pitchRad);
        float halfFov = _mainCam.fieldOfView * 0.5f * Mathf.Deg2Rad;
        float aspect = _mainCam.aspect;

        float halfViewHeight = Mathf.Tan(halfFov) * forwardOffset;
        float halfViewWidth = Mathf.Tan(halfViewHeight * aspect);

        pos.x = Mathf.Clamp(pos.x, _minBounds.x + halfViewWidth, _maxBounds.x - halfViewWidth);
        pos.z = Mathf.Clamp(pos.z, _minBounds.z + halfViewHeight, _maxBounds.z - halfViewHeight);

        transform.position = pos;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
}
