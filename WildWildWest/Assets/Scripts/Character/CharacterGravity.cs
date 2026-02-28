using UnityEngine;

public class CharacterGravity : MonoBehaviour
{
    private  float _gravityForce;
    private  float _minVerticalVelocity;
    private float _verticalVelocity;

    public float VerticalVelocity => _verticalVelocity;

    public void Initialize(float gravityForce, float minVerticalVelocity)
    {
        _gravityForce = gravityForce;
        _minVerticalVelocity = minVerticalVelocity;
    }

    public void GravityHandling(bool isGrounded)
    {
        if (isGrounded == false)
            _verticalVelocity -= _gravityForce * Time.deltaTime;
        else
            _verticalVelocity = _minVerticalVelocity;
    }
}