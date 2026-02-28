using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimator : MonoBehaviour
{
    private static readonly int VelXHash = Animator.StringToHash("VelX");
    private static readonly int VelYHash = Animator.StringToHash("VelY");
    private static readonly int CanMeleAttack = Animator.StringToHash("CanMeleAttack");
    private static readonly int IsMeleWeapon = Animator.StringToHash("IsMeleWeapon");
    private static readonly int IsDead = Animator.StringToHash("IsDead");

    private static readonly int UpperBodyIndex = 1;
    private static readonly float MinLAyerWeihgt = 0;

    private Animator _animator;

    public void Initialize()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetRunAnimation(Vector3 moveDirection)
    {
        Vector3 newMoveDirection = new Vector3(moveDirection.x, 0f, moveDirection.y);
        Vector3 localMove = transform.InverseTransformDirection(newMoveDirection);

        _animator.SetFloat(VelXHash, localMove.x, 0.1f, Time.deltaTime);
        _animator.SetFloat(VelYHash, localMove.z, 0.1f, Time.deltaTime);
    }

    public void SetMeleAttackAnimation(bool canAttack)
    {
        _animator.SetBool(CanMeleAttack, canAttack);
    }

    public void SetIdleAnimation(bool isMeleWeapon)
    {
        _animator.SetBool(IsMeleWeapon, isMeleWeapon);
    }

    public void SetDeadAnimation()
    {
        _animator.SetLayerWeight(UpperBodyIndex, MinLAyerWeihgt);

        _animator.SetBool(IsDead, true);
    }
}