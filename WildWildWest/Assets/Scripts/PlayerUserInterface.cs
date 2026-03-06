using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUserInterface : MonoBehaviour
{
    [SerializeField] private WaveSequence _waveSequence;
    [SerializeField] private Health _health;
    [SerializeField] private LevelReset _levelRestet;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private Button _restartButton;
    [SerializeField] private TextMeshProUGUI _waveText;
    [SerializeField] private TextMeshProUGUI _enemiesText;
    [SerializeField] private TextMeshProUGUI _bossesText;
    [SerializeField] private TextMeshProUGUI _loseText;
    [SerializeField] private TextMeshProUGUI _winText;

    private void Awake()
    {
        _restartButton.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _waveSequence.TextChanged += ChangeWaveText;
        _waveSequence.WavesComleted += OpenWinPanel;
        _health.HealthChanged += _healthBar.SetHealth;
        _health.OnDead += OpenLosePanel;
        _restartButton.onClick.AddListener(_levelRestet.RestartLevel);
    }

    private void OnDisable()
    {
        _waveSequence.TextChanged -= ChangeWaveText;
        _waveSequence.WavesComleted -= OpenWinPanel;
        _health.HealthChanged -= _healthBar.SetHealth;
        _health.OnDead -= OpenLosePanel;
        _restartButton.onClick.RemoveListener(_levelRestet.RestartLevel);
    }

    private void OpenLosePanel()
    {
        _restartButton.gameObject.SetActive(true);
        _loseText.gameObject.SetActive(true);
    }

    private void OpenWinPanel()
    {
        _restartButton.gameObject.SetActive(true);
        _winText.gameObject.SetActive(true);
    }

    private void ChangeWaveText()
    {
        _waveText.text = $"Wave: {_waveSequence.WaveIndex + 1}";
        _enemiesText.text = $"Enemies: {_waveSequence.EnemiesAlive}";
        _bossesText.text = $"Bosses: {_waveSequence.BossesAlive}";
    }
}