using UnityEngine;

public class Attacker : MonoBehaviour
{
    private WeaponChanger _weaponChanger;
    private Weapon _activeWeapon;

    public bool IsMelee => _activeWeapon != null && _activeWeapon.WeaponType == WeaponCategory.Melee;

    private void OnDisable()
    {
        if (_weaponChanger != null)
            _weaponChanger.OnWeaponChanged -= UpdateActiveWeapon;
    }

    public void Initialize(WeaponChanger changer)
    {
        _weaponChanger = changer;
        _activeWeapon = _weaponChanger.CurrentWeapon;

        _weaponChanger.OnWeaponChanged += UpdateActiveWeapon;
    }

    public void Attack()
    {
        if (_activeWeapon != null)
            _activeWeapon.Attack();
    }

    private void UpdateActiveWeapon(Weapon newWeapon)
    {
        _activeWeapon = newWeapon;
    }
}