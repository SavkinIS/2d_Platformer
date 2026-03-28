using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private float _maxHealth = 100f;

    public float _currentHealth;

    public event Action Dead;
    public event Action Hurted;
    public event Action HealthRestored;
    public float CurrentHealth => _currentHealth;

    public bool IsAlive => _currentHealth > 0;

    public float MaxHealth => _maxHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float amount)
    {
        if (amount < 0)
            return;

        _currentHealth -= amount;

        if (_currentHealth <= 0)
        {
            Die();
        }
        else
        {
            Hurt();
        }
    }

    public void AidKitCollected(AidKit aidKit)
    {
        if (aidKit == null || aidKit.HealthRestore < 0)
            return;

        float newHealth = _currentHealth + aidKit.HealthRestore;

        _currentHealth = Mathf.Clamp(newHealth, _currentHealth, _maxHealth);
        HealthRestored?.Invoke();
    }

    private void Hurt()
    {
        Hurted?.Invoke();
    }

    private void Die()
    {
        Dead?.Invoke();
    }
}