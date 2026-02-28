using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUserInterface : MonoBehaviour
{
    [SerializeField] private WaveManager _waveManager;
    [SerializeField] private CharacterHealth _health;
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
        _waveManager.OnTextChanged += ChangeWaveText;
        _waveManager.OnWin += OpenWinPanel;
        _health.HealthChanged += _healthBar.SetHealth;
        _health.OnDead += OpenLosePanel;
        _restartButton.onClick.AddListener(_levelRestet.RestartLevel);
    }

    private void OnDisable()
    {
        _waveManager.OnTextChanged -= ChangeWaveText;
        _waveManager.OnWin -= OpenWinPanel;
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
        _waveText.text = $"Wave: {_waveManager.WaveIndex + 1}";
        _enemiesText.text = $"Enemies: {_waveManager.EnemiesAlive}";
        _bossesText.text = $"Bosses: {_waveManager.BossesAlive}";
    }
}