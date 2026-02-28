using System.Collections.Generic;
using UnityEngine;

public class CharacterHandHandler : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Transform _bulletContainer;
    [SerializeField] private Transform _aimTarget;
    [SerializeField] private Transform _meleAttackZone;

    private Weapon _currentWeapon;
    private int _currentIndex;

    private bool _isMeleWeapon;
    public bool IsMeleWeapon => _isMeleWeapon;

    public void SetData(Transform bulletContainer, Transform aimTarget)
    {
        _bulletContainer = bulletContainer;
        _aimTarget = aimTarget;
    }

    public void Initialize()
    {
        if (_weapons.Count > 1)
        {
            for (int i = 1; i < _weapons.Count; i++)
            {
                InitializeWeapons(i);
            }
        }

        _currentWeapon = _weapons[0];
        InitializeWeapons(0);
        _currentWeapon.gameObject.SetActive(true);
        _currentIndex = 0;

        _isMeleWeapon = _currentWeapon.IsMeleWeapon;
    }

    public void ChangeWeapon()
    {
        _currentWeapon.gameObject.SetActive(false);
        _currentIndex = (_currentIndex + 1) % _weapons.Count;

        _currentWeapon = _weapons[_currentIndex];
        _currentWeapon.gameObject.SetActive(true);

        _isMeleWeapon = _currentWeapon.IsMeleWeapon;
    }

    public void Attack()
    {
        _currentWeapon.Attack();
    }

    public void DamageToCharacter()
    {
        _currentWeapon.DamageToCharacter();
    }

    public void DeactivateWeapon()
    {
        _currentWeapon.gameObject.SetActive(false);
    }

    private void InitializeWeapons(int weaponIndex)
    {
        if (_weapons[weaponIndex].TryGetComponent(out Sword sword))
        {
            sword.Initialize(_meleAttackZone);
            sword.gameObject.SetActive(false);
        }
        else if (_weapons[weaponIndex].TryGetComponent(out Rifle rifle))
        {
            rifle.Initialize(_bulletContainer, _aimTarget);
            rifle.gameObject.SetActive(false);
        }
    }
}