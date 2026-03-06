using UnityEngine;

[RequireComponent(typeof(WeaponChanger))]
[RequireComponent(typeof(Attacker))]
public class CharacterHandHandler : MonoBehaviour
{
    [SerializeField] private Transform _bulletContainer;
    [SerializeField] private Transform _aimTarget;
    [SerializeField] private Transform _meleAttackZone;

    private WeaponChanger _weaponChanger;
    private Attacker _attacker;

    private bool _canWork;

    public bool IsMeleeWeapon => _attacker.IsMelee;

    public void SetData(Transform bulletContainer, Transform aimTarget)
    {
        _bulletContainer = bulletContainer;
        _aimTarget = aimTarget;
    }

    public void Initialize()
    {
        GetComponents();

        _weaponChanger.Initialize(_bulletContainer, _aimTarget, _meleAttackZone);
        _attacker.Initialize(_weaponChanger);

        _canWork = true;
    }

    public void ChangeWeapon()
    {
        if (_canWork)
            _weaponChanger.NextWeapon();
    }

    public void Attack()
    {
        if (_canWork)
            _attacker.Attack();
    }

    public void Deactivate()
    {
        _canWork = false;
    }

    private void GetComponents()
    {
        _weaponChanger = GetComponent<WeaponChanger>();
        _attacker = GetComponent<Attacker>();
    }
}