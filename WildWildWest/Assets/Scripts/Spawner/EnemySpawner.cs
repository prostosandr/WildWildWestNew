using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner<Enemy, EnemyData>
{
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private Transform _bulletContainer;

    public void Initialize(int capacity, int maxSize)
    {
        Initialize(Container, Prefab, capacity, maxSize);
    }

    public void Spawn(Transform target)
    {
        Transform spawnPoint = _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Count)];

        CurrentItem = Pool.Get();
        CurrentItem.Initialize(target, spawnPoint.position, _bulletContainer);
        CurrentItem.gameObject.SetActive(true);
    }
}