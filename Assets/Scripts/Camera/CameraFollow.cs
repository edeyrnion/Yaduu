using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform _player;

    Vector3 _cameraOffset;

    private float _minCameraAngleY = -20.0f;
    private float _maxCameraAngleY = 25.0f;
    private float _rotationSensX = 300.0f;
    private float _rotationSensY = 200.0f;
    private float _rotationY = 0.0f;
    private float _rotationX = 0.0f;
    private Quaternion _rVelocity;
    private Quaternion _desiredCameraRotation;

    private float _maxCameraDistance = 1.5f;
    private float _minCameraDistance = 0.3f;
    private float _scrollSens = 200.0f;
    private float _scroll;
    private Vector3 _sVelocity;
    private Vector3 _desiredCameraDistance;

    private void Start()
    {
        if (_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        _cameraOffset = new Vector3(0, 1, 0);
        transform.position = _player.position + _cameraOffset;
        _desiredCameraDistance = transform.localScale;

        MouseManager.Instance.OnScrollWheelClick.AddListener(HandleCameraRotation);
        MouseManager.Instance.OnScrollWheel.AddListener(HandleCameraZoom);
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _player.transform.position + _cameraOffset, Time.deltaTime * 5);

        transform.localScale = Smooth.SmoothDamp(transform.localScale, _desiredCameraDistance, ref _sVelocity, 0.3f);

        transform.rotation = Smooth.SmoothDamp(transform.rotation, _desiredCameraRotation, ref _rVelocity, 0.3f);
    }

    private void HandleCameraRotation(Vector3 target, Layer layer)
    {
        _rotationX += Input.GetAxis("Mouse X") * _rotationSensX * Time.deltaTime;
        _rotationY += Input.GetAxis("Mouse Y") * _rotationSensY * Time.deltaTime;
        _rotationY = Mathf.Clamp(_rotationY, _minCameraAngleY, _maxCameraAngleY);
        _desiredCameraRotation = Quaternion.Euler(_rotationY, _rotationX, 0f);
    }

    private void HandleCameraZoom(float scroll)
    {
        _scroll += -scroll * _scrollSens * Time.deltaTime;
        _scroll = Mathf.Clamp(_scroll, _minCameraDistance, _maxCameraDistance);
        _desiredCameraDistance = new Vector3(_scroll, _scroll, _scroll);
    }
}
