using UnityEngine;

public class BulletSpawner : Spawner<Bullet, BulletData>
{
    public override void Spawn(Transform spawnerPosition, BulletData data)
    {
        _currentItem = _pool.Get();
        _currentItem.transform.position = spawnerPosition.position;
        _currentItem.transform.rotation = spawnerPosition.rotation;
        _currentItem.gameObject.SetActive(true);
        _currentItem.Initialize(spawnerPosition.forward, data.Speed, data.LifeTime, data.Damage);
    }
}