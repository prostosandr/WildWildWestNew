using UnityEngine;

[RequireComponent(typeof(MuzzleRotator))]
[RequireComponent(typeof(BulletSpawner))]
[RequireComponent(typeof(AudioSource))]
public class Rifle : Weapon
{
    [SerializeField] private RifleScriptableObject _settings;
    [SerializeField] private ParticleSystem _gunSmoke;
    [SerializeField] private Transform _muzzle;
    [SerializeField] private Bullet _prefab;

    private BulletData _bulletData;
    private MuzzleRotator _muzzleRotator;
    private BulletSpawner _bulletSpawner;
    private AudioSource _audioSource;
    private AudioClip _shootClip;

    private float _delayBetweenShoots;
    private float _lastShootTime;

    public override void Initialize(Transform bulletContainer, Transform aimTarget)
    {
        _muzzleRotator = GetComponent<MuzzleRotator>();
        _bulletSpawner = GetComponent<BulletSpawner>();
        _audioSource = GetComponent<AudioSource>();

        _bulletData = new BulletData
        {
            Speed = _settings.BulletSpeed,
            LifeTime = _settings.BulletLifeTime,
            Damage = _settings.BulletDamage,
        };

        _shootClip = _settings.ShootClip;
        _delayBetweenShoots = _settings.DelayBetweenShoots;

        _muzzleRotator.Initialize(_muzzle, aimTarget, _settings.MinAimDistance);
        _bulletSpawner.Initialize(bulletContainer, _prefab, _settings.PoolCapacity, _settings.PoolMaxSize);

        _isMeleWeapon = false;
    }

    public void TakeAim()
    {
        _muzzleRotator.Rotate();
    }

    public override void Attack()
    {
        if (Time.time >= _lastShootTime + _delayBetweenShoots)
        {
            _bulletSpawner.Spawn(_muzzle, _bulletData);
            _gunSmoke.Play();
            _audioSource.pitch = Random.Range(0.8f, 1.1f);
            _audioSource.PlayOneShot(_shootClip);

            _lastShootTime = Time.time;
        }
    }
}