using UnityEngine;

public class BulletSpawner : Spawner<Bullet, BulletData>
{
    public override void Spawn(Transform spawnerPosition, BulletData data)
    {
        CurrentItem = Pool.Get();
        CurrentItem.transform.position = spawnerPosition.position;
        CurrentItem.transform.rotation = spawnerPosition.rotation;
        CurrentItem.gameObject.SetActive(true);
        CurrentItem.Initialize(spawnerPosition.forward, data.Speed, data.LifeTime, data.Damage);
    }
}