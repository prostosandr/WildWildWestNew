using UnityEngine;

[CreateAssetMenu(fileName = nameof(Sword), menuName = Constants.Configs.SwordMenuName + nameof(SwordConfig))]
public class SwordConfig : ScriptableObject
{
    [Header("Sword config")]
    [SerializeField] public float Damage;
    [SerializeField] public float AttackZoneRadius;
    [SerializeField] public float AttackColdown;
}