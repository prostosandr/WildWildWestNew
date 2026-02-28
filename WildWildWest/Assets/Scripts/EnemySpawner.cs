using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner<Enemy, EnemyData>
{
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private Transform _target;
    [SerializeField] private Enemy _prefabEnemy;
    [SerializeField] private Transform _enemysContainer;
    [SerializeField] private Transform _bulletContainer;
    [SerializeField] int capacity;
    [SerializeField] private int maxSize;

    public event Action OnDisabled;

    private void Awake()
    {
        Initialize(_enemysContainer, _prefabEnemy, capacity, maxSize);
    }

    public override void Spawn()
    {
        Transform spawnPoint = _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Count)];

        _currentItem = _pool.Get();
        _currentItem.Initialize(_target, spawnPoint.position, _bulletContainer);
        _currentItem.gameObject.SetActive(true);
    }

    protected override void ReleaseItem(Enemy item)
    {
        base.ReleaseItem(item);

        OnDisabled?.Invoke();
    }
}