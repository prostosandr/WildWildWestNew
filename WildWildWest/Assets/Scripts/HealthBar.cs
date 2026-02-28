using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image _healthBarImage;

    private void Awake()
    {
        _healthBarImage = GetComponent<Image>();
    }

    public void SetHealth(float health)
    {
        _healthBarImage.fillAmount = health / 100f;
    }
}
