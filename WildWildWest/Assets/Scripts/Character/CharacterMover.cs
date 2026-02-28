using UnityEngine;

public class CharacterMover : MonoBehaviour 
{
    private Vector3 _currentVelocity;
    private Vector3 _smoothVelocity;
    [SerializeField] private float _smoothTime = 0.1f;

    private CharacterController _controller;
    private float _moveSpeed;

    public void Initialize(CharacterController controller, float moveSpeed)
    {
        _controller = controller;
        _moveSpeed = moveSpeed;
    }

    public void Move(Vector2 moveDirection, float verticalVelocity)
    {
        Vector3 targetVelocity = new Vector3(moveDirection.x, 0f, moveDirection.y) * _moveSpeed;
        _currentVelocity = Vector3.SmoothDamp(_currentVelocity, targetVelocity, ref _smoothVelocity, _smoothTime);

        Vector3 finalMoveDirection = _currentVelocity;
        finalMoveDirection.y = verticalVelocity;

        _controller.Move(finalMoveDirection * Time.deltaTime);
    }
}