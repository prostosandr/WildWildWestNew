using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChanger : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weapons;

    private int _currentIndex;
    private Weapon _currentWeapon;

    public event Action<Weapon> OnWeaponChanged;

    public Weapon CurrentWeapon => _currentWeapon;

    public void Initialize(Transform bulletContainer, Transform aimTarget, Transform meleeZone)
    {
        for (int i = 0; i < _weapons.Count; i++)
        {
            InitializeWeapon(_weapons[i], bulletContainer, aimTarget, meleeZone);
            _weapons[i].gameObject.SetActive(false);
        }

        Equip(0);
    }

    public void NextWeapon()
    {
        _currentIndex = (_currentIndex + 1) % _weapons.Count;
        Equip(_currentIndex);
    }

    private void Equip(int index)
    {
        if (_currentWeapon != null) 
            _currentWeapon.gameObject.SetActive(false);

        _currentWeapon = _weapons[index];
        _currentWeapon.gameObject.SetActive(true);

        OnWeaponChanged?.Invoke(_currentWeapon);
    }

    private void InitializeWeapon(Weapon weapon, Transform bullets, Transform aim, Transform melee)
    {
        IWeaponData data = null;

        if (weapon.WeaponType == WeaponCategory.Melee)
            data = new MeleeData { AttackZone = melee };
        else if(weapon.WeaponType == WeaponCategory.Range)
            data = new RangeData { BulletContainer = bullets, AimTarget = aim };

        weapon.Initialize(data);
    }
}
