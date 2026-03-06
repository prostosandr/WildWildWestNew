using System;
using UnityEngine;

public class WaveDirector : MonoBehaviour
{
    [SerializeField] private WavesConfig _settings;

    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private EnemySpawner _bossSpawner;

    [SerializeField] private Transform _target;

    [SerializeField] private int _capacity;
    [SerializeField] private int _maxSize;

    private int _waveIndex;
    private int _enemiesToSpawn;
    private int _bossesToSpawn;
    private int _enemiesAlive;
    private int _bossesAlive;
    private WaveState _currentState;

    public int WaveIndex => _waveIndex;
    public int EnemiesAlive => _enemiesAlive;
    public int BossesAlive => _bossesAlive;

    public event Action WavesComleted;
    public event Action TextChanged;

    private void Awake()
    {
        _enemySpawner.Initialize(_capacity, _maxSize);
        _bossSpawner.Initialize(_capacity, _maxSize);
    }

    private void Start()
    {
        _waveIndex = 0;
        LoadWaveData();
        _currentState = WaveState.SpawningEnemies;
    }

    private void Update()
    {
        switch (_currentState)
        {
            case WaveState.SpawningEnemies:
                SpawningEnemies();
                break;

            case WaveState.WaitingForEnemies:
                WaitingForEnemies();
                break;

            case WaveState.SpawningBoss:
                SpawningBoss();
                break;

            case WaveState.WaitingForBoss:
                WaitingForBoss();
                break;

            case WaveState.WaveComplete:
                WaveComplete();
                break;
        }
    }

    private void SpawningEnemies()
    {
        for (int i = 0; i < _enemiesToSpawn; i++)
        {
            _enemySpawner.Spawn(_target);
            _enemySpawner.CurrnetItem.Deactivated += OnEnemyDied;
        }

        _enemiesAlive = _enemiesToSpawn;

        TextChanged?.Invoke();

        _currentState = WaveState.WaitingForEnemies;
    }

    private void WaitingForEnemies()
    {
        if (_enemiesAlive <= 0)
        {
            if (_bossesToSpawn > 0)
                _currentState = WaveState.SpawningBoss;
            else
                _currentState = WaveState.WaveComplete;
        }
    }

    private void SpawningBoss()
    {
        for (int i = 0; i < _bossesToSpawn; i++)
        {
            _bossSpawner.Spawn(_target);
            _bossSpawner.CurrnetItem.Deactivated += OnBossDied;
        }

        _bossesAlive = _bossesToSpawn;

        TextChanged?.Invoke();

        _currentState = WaveState.WaitingForBoss;
    }

    private void WaitingForBoss()
    {
        if (_bossesAlive <= 0)
        {
            _currentState = WaveState.WaveComplete;
        }
    }

    private void WaveComplete()
    {
        _waveIndex++;

        if (_waveIndex < _settings.Waves.Count)
        {
            LoadWaveData();
            _currentState = WaveState.SpawningEnemies;
        }
        else
        {
            WavesComleted?.Invoke();
            enabled = false;
        }
    }

    private void LoadWaveData()
    {
        var wave = _settings.Waves[_waveIndex];
        _enemiesToSpawn = wave.NumberOfEnemys;
        _bossesToSpawn = wave.NumberOfBosses;

        TextChanged?.Invoke();
    }

    private void OnEnemyDied(Enemy enemy)
    {
        _enemiesAlive--;
        TextChanged?.Invoke();
        enemy.Deactivated -= OnEnemyDied;
    }

    private void OnBossDied(Enemy boss)
    {
        _bossesAlive--;
        TextChanged?.Invoke();
        boss.Deactivated -= OnBossDied;
    }
}