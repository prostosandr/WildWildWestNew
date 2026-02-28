using UnityEngine;

[CreateAssetMenu(fileName = "SwordSettings", menuName = "CharacterSettings/SwordSettings")]
public class SwordScriptableObject : ScriptableObject
{
    [Header("Sword sound")]
    [SerializeField] public AudioClip SwordClip;

    [Header("Sword config")]
    [SerializeField] public float Damage;
    [SerializeField] public float AttackZoneRadius;
    [SerializeField] public float AttackColdown;
}