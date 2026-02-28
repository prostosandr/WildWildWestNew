using System;
using UnityEngine;

public class CharacterHealth: MonoBehaviour
{
    private float _health;

    public event Action<float> HealthChanged;
    public event Action OnDead;

    public void Initialize(float health)
    {
        _health = health;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            _health = 0;

            OnDead?.Invoke();
        }

        HealthChanged?.Invoke(_health);
    }
}