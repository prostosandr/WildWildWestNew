using UnityEngine;

public class BulletMover : MonoBehaviour
{
    private float _speed;
    private Rigidbody _rigidbody;

    public void Initialize(Rigidbody rigidbody, float speed)
    {
        _rigidbody = rigidbody;
        _speed = speed;
    }

    public void SetDirection(Vector3 direction)
    {
        _rigidbody.linearVelocity = direction * _speed;
    }

    public void Deactivate()
    {
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }
}