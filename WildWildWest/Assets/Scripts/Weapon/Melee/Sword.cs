using UnityEngine;

[RequireComponent(typeof(AudioPlayer))]
public class Sword : Weapon
{
    [SerializeField] private SwordConfig _settigs;

    private Transform _attackZone;
    private AudioPlayer _audioPlayer;

    private float _lastTimeAttack;

    private Vector3 _contactPoint;
    private Vector3 _direction;
    private Quaternion _bloodRotation;

    private readonly Collider[] _hitResult = new Collider[10];

    public override WeaponCategory WeaponType => WeaponCategory.Melee;

    public override void Initialize(IWeaponData weaponData)
    {
        GetComponents();

        MeleeData swordData = (MeleeData)weaponData;

        _attackZone = swordData.AttackZone;
    }

    public override void Attack()
    {
        if (Time.time < _lastTimeAttack + _settigs.AttackColdown)
            return;

        int numberOfHit = Physics.OverlapSphereNonAlloc(_attackZone.position, _settigs.AttackZoneRadius, _hitResult);

        for(int i = 0; i < numberOfHit; i++)
        {
            Collider hit = _hitResult[i];

            if (hit.gameObject.TryGetComponent(out IDamageble damagebleItem))
            {
                _contactPoint = hit.ClosestPoint(_attackZone.transform.position);
                _direction = (_contactPoint - _attackZone.transform.position).normalized;
                _bloodRotation = Quaternion.LookRotation(_direction);
                damagebleItem.TakeDamage(_settigs.Damage, _contactPoint, _bloodRotation);
            }
        }

        _audioPlayer.Play(AudioType.MeleeAttack);

        _lastTimeAttack = Time.time;
    }

    private void GetComponents()
    {
        _audioPlayer = GetComponent<AudioPlayer>();
    }

    private void OnDrawGizmos()
    {
        if (_attackZone == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_attackZone.position, _settigs.AttackZoneRadius);
    }
}