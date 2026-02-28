using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Enemy : Character, IPolledObject<Enemy>
{
    [SerializeField] private MultiAimConstraint _spineIK;
    [SerializeField] private MultiAimConstraint _elbowIK;
    [SerializeField] private float _minDictanceToTarget;
    [SerializeField] private float _timeAfterDeath;

    private Transform _target;
    private WaitForSeconds _wait;

    private Vector2 _targetDirection;
    private bool _canShoot;

    public event Action<Enemy> Deactivated;

    protected override void Awake()
    {
        base.Awake();

        _minDictanceToTarget *= _minDictanceToTarget;

        _wait = new WaitForSeconds(_timeAfterDeath);
    }

    protected override void Update()
    {
        if (_canLiving)
        {
            _targetDirection = GetTargetDirection();

            if (_canShoot)
                _handler.Attack();

            base.Update();

            _audio.PlayStepsClip(_targetDirection.sqrMagnitude > 0.1f);
        }
    }

    public void Initialize(Transform target, Vector3 position, Transform bulletContainer)
    {
        _characterController.enabled = false;

        _target = target;
        transform.position = position;

        _characterController.enabled = true;
        _rigBuilder.enabled = true;

        _handler.SetData(bulletContainer, target);
        _handler.Initialize();

        _spineIK = SetTargetIK(_spineIK);
        _elbowIK = SetTargetIK(_elbowIK);

        _rigBuilder.Build();
    }

    protected override void HandleMoving()
    {
        base.HandleMoving();

        _mover.Move(_targetDirection, _gravity.VerticalVelocity);
        _rotator.Rotate(_target.position);
    }

    protected override void UpdateAnimations()
    {
        _animator.SetRunAnimation(_targetDirection);
        _animator.SetIdleAnimation(_handler.IsMeleWeapon);
    }

    protected override void Die()
    {
        base.Die();

        StartCoroutine(StartCountdownToDeactivation());
    }

    private Vector2 GetTargetDirection()
    {
        Vector3 direction = _target.position - transform.position;

        if (direction.sqrMagnitude > _minDictanceToTarget)
        {
            _canShoot = false;

            Vector3 noramalizedDirection = direction.normalized;

            Vector2 newDirection = new Vector2(noramalizedDirection.x, noramalizedDirection.z);

            return newDirection;
        }

        _canShoot = true;

        return Vector2.zero;
    }

    private MultiAimConstraint SetTargetIK(MultiAimConstraint constraint)
    {
        var sources = constraint.data.sourceObjects;
        sources.SetTransform(0, _target);
        constraint.data.sourceObjects = sources;

        return constraint;
    }

    private IEnumerator StartCountdownToDeactivation()
    {
        yield return _wait;

        Deactivated?.Invoke(this);
    }
}