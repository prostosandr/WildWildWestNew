using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Sword : Weapon
{
    [SerializeField] private SwordScriptableObject _settigs;

    private Transform _attackZone;
    private AudioSource _audioSource;

    private float _lastTimeAttack;

    private Character _currentCharacter;
    private Vector3 _contactPoint;
    private Quaternion _bloodRotation;

    private bool _isCharacterAttack;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        _isMeleWeapon = true;
        _isCharacterAttack = false;
    }

    public override void Initialize(Transform attackZone)
    {
        _attackZone = attackZone;
    }

    public override void Attack()
    {
        if (Time.time >= _lastTimeAttack + _settigs.AttackColdown)
        {
            Collider[] hitEnemies = Physics.OverlapSphere(_attackZone.position, _settigs.AttackZoneRadius);

            foreach (Collider hit in hitEnemies)
            {
                if (hit.gameObject.TryGetComponent(out Character character))
                {
                    _currentCharacter = character;
                    _isCharacterAttack = true;
                    _contactPoint = hit.ClosestPoint(_attackZone.transform.position);
                    Vector3 direction = (_contactPoint - _attackZone.transform.position).normalized;
                    Quaternion _bloodRotation = Quaternion.LookRotation(direction);
                }
                else
                {
                    _isCharacterAttack = false;
                    _currentCharacter = null;
                }
            }

            _lastTimeAttack = Time.time;
        }
    }

    public override void DamageToCharacter()
    {
        _audioSource.PlayOneShot(_settigs.SwordClip);

        if (_isCharacterAttack)
        {
            _currentCharacter.TakeDamage(_settigs.Damage, _contactPoint, _bloodRotation);
            _isCharacterAttack = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (_attackZone == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_attackZone.position, _settigs.AttackZoneRadius);
    }
}