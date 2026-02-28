using UnityEngine;

[CreateAssetMenu(fileName = "RifleSettings", menuName = "CharacterSettings/RifleSettings")]
public class RifleScriptableObject : ScriptableObject
{
    [Header("Rifle config")]
    public float BulletDamage;
    public float BulletSpeed;
    public float BulletLifeTime;
    public float MinAimDistance;

    [Header("Bullet spawner config")]
    public float DelayBetweenShoots;
    public int PoolCapacity;
    public int PoolMaxSize;

    [Header("Audio")]
    public AudioClip ShootClip;
}
