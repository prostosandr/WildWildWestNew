using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BulletMover))]
[RequireComponent(typeof(BulletLifeCycle))]
public class Bullet : MonoBehaviour, IPolledObject<Bullet>
{
    private BulletMover _mover;
    private BulletLifeCycle _lifeCycle;
    private Rigidbody _rigidbody;

    public event Action<Bullet> Deactivated;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _mover = GetComponent<BulletMover>();
        _lifeCycle = GetComponent<BulletLifeCycle>();
    }

    private void OnEnable()
    {
        _lifeCycle.Deactivated += Deactivate;
    }

    private void OnDisable()
    {
        _lifeCycle.Deactivated -= Deactivate;
    }

    public void Initialize(Vector3 direction, float speed, float lifeTime, float damage)
    {    
        _mover.Initialize(_rigidbody, speed);
        _lifeCycle.Initialize(lifeTime, damage);

        _mover.SetDirection(direction);  
    }

    private void Deactivate()
    {
        _mover.Deactivate();

        Deactivated?.Invoke(this);
    }
}