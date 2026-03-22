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

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public bool IsAlive  => _currentHealth > 0;
    public float MaxHealth => _maxHealth;

    public void TakeDamage(float amount)
    {
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

    private void Hurt()
    {
        Hurted?.Invoke();
    }

    private void Die()
    {
        Dead?.Invoke();
    }

    public void AidKitCollected(AidKit aidKit)
    {
        _currentHealth = Mathf.Min((_currentHealth += aidKit.HealthRestore),  _maxHealth);
        HealthRestored?.Invoke();
    }
}