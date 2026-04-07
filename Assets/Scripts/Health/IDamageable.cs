using UnityEngine;

public interface IDamageable
{
    bool IsAlive { get; }

    void TakeDamage(float damage, bool withAnimation = true);
    
    Transform Transform { get; }
}