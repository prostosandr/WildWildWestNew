using UnityEngine;

public class Player : Character
{
    [SerializeField] private PlayerInputProvider _input;

    protected override void OnEnable()
    {
        base.OnEnable();

        _handler.Initialize();
    }

    protected override void Update()
    {
        if (_canLiving)
        {
            base.Update();
            HandleChangingWeapon();

            if (_input.IsLMBPressed)
                _handler.Attack();

            _audio.PlayStepsClip(_input.MoveDirection.sqrMagnitude > 0.1f);
        }
    }

    protected override void HandleMoving()
    {
        base.HandleMoving();

        _mover.Move(_input.MoveDirection, _gravity.VerticalVelocity);
        _rotator.Rotate(_input.CursorPosition);
    }

    protected override void UpdateAnimations()
    {
        _animator.SetRunAnimation(_input.MoveDirection);
        _animator.SetIdleAnimation(_handler.IsMeleWeapon);
        _animator.SetMeleAttackAnimation(_input.IsLMBPressed && _handler.IsMeleWeapon);
    }

    private void HandleChangingWeapon()
    {
        if (_input.IsQPressed)
        {
            _handler.ChangeWeapon();

            if (_handler.IsMeleWeapon)
            {
                _rigBuilder.enabled = false;
                _rotator.SetAngle(_settings.MeleAngleOffset);
            }
            else
            {
                _rigBuilder.enabled = true;
                _rotator.SetAngle(_settings.FirearmsAngleOffset);
            }
        }
    }
}