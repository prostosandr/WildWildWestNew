using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputProvider : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _chestPosition;

    private PlayerInput _input;

    private Vector3 _cursorPosition;

    private bool _isLMBPressed;
    private bool _isQPressed;

    public Vector2 MoveDirection => _input.Player.Move.ReadValue<Vector2>();
    public Vector3 CursorPosition => _cursorPosition;

    public bool IsLMBPressed => _isLMBPressed;
    public bool IsQPressed => _isQPressed;

    private void Awake()
    {
        _input = new PlayerInput();
    }

    private void OnEnable()
    {
        _input.Enable();

        _input.Player.Shoot.performed += OnPressedLMB;
        _input.Player.Shoot.canceled += OnReleasedLMB;
        _input.Player.SwitchWeapon.performed += OnPressedQ;
    }

    private void Update()
    {
        _cursorPosition = GetCursorPosition();
    }

    private void LateUpdate()
    {
        _isQPressed = false;
    }

    private void OnDisable()
    {
        _input.Disable();

        _input.Player.Shoot.performed -= OnPressedLMB;
        _input.Player.Shoot.canceled -= OnReleasedLMB;
        _input.Player.SwitchWeapon.performed -= OnPressedQ;
    }

    private void OnPressedLMB(InputAction.CallbackContext context)
    {
        _isLMBPressed = true;
    }

    private void OnReleasedLMB(InputAction.CallbackContext context)
    {
        _isLMBPressed = false; 
    }

    private void OnPressedQ(InputAction.CallbackContext context)
    {
        _isQPressed = true;
    }

    private Vector3 GetCursorPosition()
    {
        Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        Plane groundPlane = new Plane(Vector3.up, _chestPosition.position);

        if (groundPlane.Raycast(ray, out float rayDistance))
        {
            Vector3 targetPosition = ray.GetPoint(rayDistance);

            return targetPosition;
        }

        return Vector3.zero;
    }
}
