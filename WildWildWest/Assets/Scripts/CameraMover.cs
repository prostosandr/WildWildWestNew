using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] Transform _player;

    [SerializeField] float _returnSpeed;
    [SerializeField] float _height;
    [SerializeField] float _rearDistance;

    private Vector3 _currentVector;

    private void Start()
    {
        transform.position = new Vector3(_player.position.x, _player.position.y + _height, _player.position.z - _rearDistance);
        transform.rotation = Quaternion.LookRotation(_player.position - transform.position);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        _currentVector = new Vector3(_player.position.x, _player.position.y + _height, _player.position.z - _rearDistance);
        transform.position = Vector3.Lerp(transform.position, _currentVector, _returnSpeed * Time.deltaTime);
    }
}
