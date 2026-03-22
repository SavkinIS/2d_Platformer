using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _enemyLayer;
    
    public void DealDamage()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            _radius,
            _enemyLayer);

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(_damage);
            }
        }
    }
}