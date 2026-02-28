using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected bool _isMeleWeapon;
    protected float _damage;

    public bool IsMeleWeapon => _isMeleWeapon;

    public virtual void Attack() { }

    public virtual void DamageToCharacter() { }

    public virtual void Initialize(Transform bulletContainer, Transform aimTarget) { }

    public virtual void Initialize(Transform meleAttackZone) { }
}