using UnityEngine;
using UnityEngine.Animations.Rigging;

[RequireComponent(typeof(RigBuilder))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CharacterGravity))]
[RequireComponent(typeof(CharacterMover))]
[RequireComponent(typeof(CharacterRotator))]
[RequireComponent(typeof(CharacterAnimator))]
[RequireComponent(typeof(AudioPlayer))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(CharacterHandHandler))]
[RequireComponent(typeof(AudioPlayer))]
public abstract class Character : MonoBehaviour, IDamageble
{
    [Header("Character components")]
    [SerializeField] protected CharacterConfig Settings;
    [SerializeField] protected ParticleSystem BloodEffect;

    protected RigBuilder RigBuilder;
    protected Collider Collider;
    protected CharacterController CharacterController;
    protected CharacterGravity Gravity;
    protected CharacterMover Mover;
    protected CharacterRotator Rotator;
    protected CharacterAnimator Animator;
    protected AudioPlayer Audio;
    protected Health Health;
    protected CharacterHandHandler Handler;
    protected AudioPlayer AudioPlayer;

    protected bool CanLiving;

    protected virtual void Awake()
    {
        RigBuilder = GetComponent<RigBuilder>();
        CharacterController = GetComponent<CharacterController>();
        Collider = GetComponent<Collider>();
        Gravity = GetComponent<CharacterGravity>();
        Mover = GetComponent<CharacterMover>();
        Rotator = GetComponent<CharacterRotator>();
        Animator = GetComponent<CharacterAnimator>();
        Audio = GetComponent<AudioPlayer>();
        Health = GetComponent<Health>();
        Handler = GetComponent<CharacterHandHandler>();
        AudioPlayer = GetComponent<AudioPlayer>();
    }

    protected virtual void OnEnable()
    {
        Health.OnDead += Die;

        Initialize();
    }

    private void OnDisable()
    {
        Health.OnDead -= Die;
    }

    protected virtual void Update()
    {
        HandleMoving();
        UpdateAnimations();
    }

    public void TakeDamage(float damage, Vector3 postion, Quaternion rotation)
    {
        Audio.Play(AudioType.TakeDamage);
        Health.TakeDamage(damage);

        BloodEffect.gameObject.transform.position = postion;
        BloodEffect.gameObject.transform.rotation = rotation;
        BloodEffect.Play();
    }

    protected void Initialize()
    {
        Gravity.Initialize(Settings.GravityForce, Settings.MinVerticalVelocity);
        Mover.Initialize(CharacterController, Settings.MoveSpeed);
        Rotator.Initialize(Settings.RotateSpeed, Settings.FirearmsAngleOffset);
        Animator.Initialize();
        Health.Initialize(Settings.Health);

        CanLiving = true;
    }

    protected virtual void HandleMoving()
    {
        Gravity.GravityHandling(CharacterController.isGrounded);
    }

    protected virtual void Die()
    {
        Audio.Play(AudioType.Death);
        CanLiving = false;
        RigBuilder.enabled = false;
        Collider.enabled = false;

        Animator.SetDeadAnimation();
        Handler.Deactivate();
    }

    protected virtual void UpdateAnimations() { }
}