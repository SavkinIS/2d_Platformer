using UnityEngine;

public interface IDamageable
{
    bool IsAlive { get; }

    float TakeDamage(float damage, bool withAnimation = true);
    
    Transform Transform { get; }
}