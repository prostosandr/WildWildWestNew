using UnityEngine;

public class MuzzleRotator : MonoBehaviour
{
    private Transform _muzzle;
    private Transform _target;
    private float _minDistance;

    public void Initialize(Transform muzzle, Transform target, float minDistance)
    {
        _muzzle = muzzle;
        _target = target;
        _minDistance = minDistance;

        _minDistance *= _minDistance;
    }

    public void Rotate()
    {
        if ((_muzzle.position - _target.position).sqrMagnitude >= _minDistance)
            _muzzle.LookAt(_target);
    }
}