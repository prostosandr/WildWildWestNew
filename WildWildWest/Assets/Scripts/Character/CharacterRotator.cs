using UnityEngine;

public class CharacterRotator : MonoBehaviour
{
    private float _rotateSpeed;
    private float _angleOffset;

    public void Initialize(float rotateSpeed, float angleOffset)
    {
        _rotateSpeed = rotateSpeed;
        _angleOffset = angleOffset;
    }

    public void SetAngle(float angle)
    {
        _angleOffset = angle;
    }

    public void Rotate(Vector3 targetPosition)
    {
        Vector3 lookDirection = targetPosition - transform.position;
        lookDirection.y = 0f;

        if (lookDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            Quaternion offset = Quaternion.Euler(0f, _angleOffset, 0f);
            Quaternion finalAngle = targetRotation * offset;
            transform.rotation = Quaternion.Slerp(transform.rotation, finalAngle, _rotateSpeed * Time.deltaTime);
        }
    }
}