using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public abstract WeaponCategory WeaponType { get; }

    public virtual void Attack() { }

    public virtual void Initialize(IWeaponData weaponData) { }
}