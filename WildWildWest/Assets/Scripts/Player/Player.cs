using UnityEngine;

public class Player : Character
{
    [SerializeField] private PlayerInputProvider _input;

    protected override void OnEnable()
    {
        base.OnEnable();

        Handler.Initialize();
    }

    protected override void Update()
    {
        if (CanLiving)
        {
            base.Update();
            HandleChangingWeapon();

            if (_input.IsLMBPressed)
                Handler.Attack();

            if (_input.MoveDirection.sqrMagnitude > 0.1f)
                AudioPlayer.Play(AudioType.Steps);
        }
    }

    protected override void HandleMoving()
    {
        base.HandleMoving();

        Mover.Move(_input.MoveDirection, Gravity.VerticalVelocity);
        Rotator.Rotate(_input.CursorPosition);
    }

    protected override void UpdateAnimations()
    {
        Animator.SetRunAnimation(_input.MoveDirection);
        Animator.SetIdleAnimation(Handler.IsMeleeWeapon);
        Animator.SetMeleAttackAnimation(_input.IsLMBPressed && Handler.IsMeleeWeapon);
    }

    private void HandleChangingWeapon()
    {
        if (_input.IsQPressed)
        {
            Handler.ChangeWeapon();

            if (Handler.IsMeleeWeapon)
            {
                RigBuilder.enabled = false;
                Rotator.SetAngle(Settings.MeleAngleOffset);
            }
            else
            {
                RigBuilder.enabled = true;
                Rotator.SetAngle(Settings.FirearmsAngleOffset);
            }
        }
    }
}