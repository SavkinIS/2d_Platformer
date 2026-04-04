using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Health : MonoBehaviour, IDamageable, IHealable, IHealth
{
    [SerializeField] private float _max = 100f; 
    
    private readonly float _min = 0f;
    
    private float _current;

    public event Action Dead;
    public event Action Hurted;
    public event Action HealthRestored;
    public event Action<float, float, float> Changed;
    public float Current => _current;

    public bool IsAlive => _current > 0;

    public float Max => _max;

    private void Awake()
    {
        _current = _max;
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0)
            return;

        float previous = _current;
        _current = Math.Clamp(_current - damage, _min, _max);
        Refresh(previous - _current);
        
        if (_current <= 0)
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

        float newHealth = _current + aidKit.HealthRestore;
        _current = Mathf.Clamp(newHealth, _current, _max);
        HealthRestored?.Invoke();
        Changed?.Invoke(Current, Max, aidKit.HealthRestore);
    }

    private void Hurt()
    {
        Hurted?.Invoke();
    }

    private void Die()
    {
        Dead?.Invoke();
    }

    public void Refresh(float changedValue = 0)
    {
        Changed?.Invoke(Current, Max, changedValue);
    }
}