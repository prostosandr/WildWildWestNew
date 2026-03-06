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
        if (CanLiving)
        {
            _targetDirection = GetTargetDirection();

            if (_canShoot)
                Handler.Attack();

            base.Update();

            if (_targetDirection.sqrMagnitude > 0.1f)
                AudioPlayer.Play(AudioType.Steps);
        }
    }

    public void Initialize(Transform target, Vector3 position, Transform bulletContainer)
    {
        CharacterController.enabled = false;

        _target = target;
        transform.position = position;

        CharacterController.enabled = true;
        RigBuilder.enabled = true;

        Handler.SetData(bulletContainer, target);
        Handler.Initialize();

        _spineIK = SetTargetIK(_spineIK);
        _elbowIK = SetTargetIK(_elbowIK);

        RigBuilder.Build();
    }

    protected override void HandleMoving()
    {
        base.HandleMoving();

        Mover.Move(_targetDirection, Gravity.VerticalVelocity);
        Rotator.Rotate(_target.position);
    }

    protected override void UpdateAnimations()
    {
        Animator.SetRunAnimation(_targetDirection);
        Animator.SetIdleAnimation(Handler.IsMeleeWeapon);
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