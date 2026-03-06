using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _target;

    [SerializeField] private float _returnSpeed;
    [SerializeField] private float _height;
    [SerializeField] private float _rearDistance;

    private Vector3 _currentVector;

    private void Start()
    {
        transform.position = new Vector3(_target.position.x, _target.position.y + _height, _target.position.z - _rearDistance);
        transform.rotation = Quaternion.LookRotation(_target.position - transform.position);
    }

    private void LateUpdate()
    {
        Move();
    }

    private void Move()
    {
        _currentVector = new Vector3(_target.position.x, _target.position.y + _height, _target.position.z - _rearDistance);
        transform.position = Vector3.Lerp(transform.position, _currentVector, _returnSpeed * Time.deltaTime);
    }
}
