using UnityEngine;

[CreateAssetMenu(fileName = nameof(RifleConfig), menuName = Constants.Configs.RifleMenuName + nameof(RifleConfig))]
public class RifleConfig : ScriptableObject
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
}