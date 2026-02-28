using System;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private WavesScriptableObject _settings;

    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private EnemySpawner _bossSpawner;

    private int _waveIndex;
    private int _enemiesToSpawn;
    private int _bossesToSpawn;
    private int _enemiesAlive;
    private int _bossesAlive;
    private WaveState _currentState;

    public int WaveIndex => _waveIndex;
    public int EnemiesAlive => _enemiesAlive;
    public int BossesAlive => _bossesAlive;

    public event Action OnWin;
    public event Action OnTextChanged;

    private enum WaveState
    {
        SpawningEnemies,
        WaitingForEnemies,
        SpawningBoss,
        WaitingForBoss,
        WaveComplete
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
            _enemySpawner.Spawn();
            _enemySpawner.CurrnetItem.Deactivated += OnEnemyDied;
        }

        _enemiesAlive = _enemiesToSpawn;

        OnTextChanged?.Invoke();

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
            _bossSpawner.Spawn();
            _bossSpawner.CurrnetItem.Deactivated += OnBossDied;
        }

        _bossesAlive = _bossesToSpawn;

        OnTextChanged?.Invoke();

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
            OnWin?.Invoke();
            enabled = false;
        }
    }

    private void LoadWaveData()
    {
        var wave = _settings.Waves[_waveIndex];
        _enemiesToSpawn = wave.NumberOfEnemys;
        _bossesToSpawn = wave.NumberOfBosses;

        OnTextChanged?.Invoke();
    }

    private void OnEnemyDied(Enemy enemy)
    {
        _enemiesAlive--;
        OnTextChanged?.Invoke();
        enemy.Deactivated -= OnEnemyDied;
    }

    private void OnBossDied(Enemy boss)
    {
        _bossesAlive--;
        OnTextChanged?.Invoke();
        boss.Deactivated -= OnBossDied;
    }
}