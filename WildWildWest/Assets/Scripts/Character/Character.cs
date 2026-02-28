using UnityEngine;
using UnityEngine.Animations.Rigging;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CharacterGravity))]
[RequireComponent(typeof(CharacterMover))]
[RequireComponent(typeof(CharacterRotator))]
[RequireComponent(typeof(CharacterAnimator))]
[RequireComponent(typeof(CharacterAudioPlayer))]
[RequireComponent(typeof(CharacterHealth))]
[RequireComponent(typeof(CharacterHandHandler))]
[RequireComponent(typeof(AudioSource))]
public abstract class Character : MonoBehaviour
{
    [Header("Character components")]
    [SerializeField] protected CharacterScriptableObject _settings;
    [SerializeField] protected RigBuilder _rigBuilder;
    [SerializeField] protected ParticleSystem _bloodEffect;

    protected Collider _collider;
    protected CharacterController _characterController;
    protected CharacterGravity _gravity;
    protected CharacterMover _mover;
    protected CharacterRotator _rotator;
    protected CharacterAnimator _animator;
    protected CharacterAudioPlayer _audio;
    protected CharacterHealth _health;
    protected CharacterHandHandler _handler;
    protected AudioSource _audioSource;

    protected bool _canLiving;

    protected virtual void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _collider = GetComponent<Collider>();
        _gravity = GetComponent<CharacterGravity>();
        _mover = GetComponent<CharacterMover>();
        _rotator = GetComponent<CharacterRotator>();
        _animator = GetComponent<CharacterAnimator>();
        _audio = GetComponent<CharacterAudioPlayer>();
        _health = GetComponent<CharacterHealth>();
        _audioSource = GetComponent<AudioSource>();
        _handler = GetComponent<CharacterHandHandler>();
    }

    protected virtual void OnEnable()
    {
        _health.OnDead += Die;

        Initialize();
    }

    private void OnDisable()
    {
        _health.OnDead -= Die;
    }

    protected virtual void Update()
    {
        HandleMoving();
        UpdateAnimations();
    }

    protected virtual void Initialize()
    {
        _gravity.Initialize(_settings.GravityForce, _settings.MinVerticalVelocity);
        _mover.Initialize(_characterController, _settings.MoveSpeed);
        _rotator.Initialize(_settings.RotateSpeed, _settings.FirearmsAngleOffset);
        _animator.Initialize();
        _audio.Initialize(_audioSource, _settings.StepsClips, _settings.TakeDamageClip, _settings.DeadClip, _settings.DelayBetweenSteps, _settings.MinPitchSteps, _settings.MaxPitchSteps);
        _health.Initialize(_settings.Health);

        _canLiving = true;
    }

    public virtual void TakeDamage(float damage, Vector3 postion, Quaternion rotation)
    {
        _audio.PlayDamageClip();
        _health.TakeDamage(damage);

        _bloodEffect.gameObject.transform.position = postion;
        _bloodEffect.gameObject.transform.rotation = rotation;
        _bloodEffect.Play();
    }

    protected virtual void HandleMoving()
    {
        _gravity.GravityHandling(_characterController.isGrounded);
    }

    protected virtual void Die()
    {
        _audio.PlayDeadClip();
        _canLiving = false;
        _rigBuilder.enabled = false;
        _collider.enabled = false;

        _animator.SetDeadAnimation();
        _handler.DeactivateWeapon();
    }

    protected virtual void UpdateAnimations() { }
}