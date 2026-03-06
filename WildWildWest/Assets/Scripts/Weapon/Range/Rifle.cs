using UnityEngine;

[RequireComponent(typeof(MuzzleRotator))]
[RequireComponent(typeof(BulletSpawner))]
[RequireComponent(typeof(AudioPlayer))]
public class Rifle : Weapon
{
    [SerializeField] private RifleConfig _settings;
    [SerializeField] private ParticleSystem _gunSmoke;
    [SerializeField] private Transform _muzzle;
    [SerializeField] private Bullet _prefab;

    private BulletData _bulletData;
    private MuzzleRotator _muzzleRotator;
    private BulletSpawner _bulletSpawner;
    private AudioPlayer _audioPlayer;

    private float _delayBetweenShoots;
    private float _lastShootTime;

    public override WeaponCategory WeaponType => WeaponCategory.Range;

    public override void Initialize(IWeaponData weaponData)
    {
        GetComponents();

        RangeData rifleData = (RangeData)weaponData;

        _bulletData = new BulletData
        {
            Speed = _settings.BulletSpeed,
            LifeTime = _settings.BulletLifeTime,
            Damage = _settings.BulletDamage,
        };

        _delayBetweenShoots = _settings.DelayBetweenShoots;

        _muzzleRotator.Initialize(_muzzle, rifleData.AimTarget, _settings.MinAimDistance);
        _bulletSpawner.Initialize(rifleData.BulletContainer, _prefab, _settings.PoolCapacity, _settings.PoolMaxSize);
    }

    public override void Attack()
    {
        if (Time.time >= _lastShootTime + _delayBetweenShoots)
        {
            _bulletSpawner.Spawn(_muzzle, _bulletData);
            _gunSmoke.Play();
            _audioPlayer.Play(AudioType.RangeWeaponShoot);

            _lastShootTime = Time.time;
        }
    }

    private void GetComponents()
    {
        _muzzleRotator = GetComponent<MuzzleRotator>();
        _bulletSpawner = GetComponent<BulletSpawner>();
        _audioPlayer = GetComponent<AudioPlayer>();
    }
}