using System;
using System.Collections.Generic;
using UnityEngine;


public class Vampirism : MonoBehaviour
{
    [SerializeField] private float _damagePerSecond = 10;
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private float _abilityRadius = 0.92f;

    private List<IDamageable> _targets = new List<IDamageable>();
    private IDamageable _target;
    private Collider2D[] _hits;
    private IHealable _healable;
    private float _givenDamage;

    public void Execute(float delta)
    { 
        FindTargets();
        FindClosestTarget();
        
        if (_target != null)
        {
            float damageTaken =  _target.TakeDamage(_damagePerSecond * delta, false);
            _healable.Heal(damageTaken);

            if (_target.IsAlive == false)
            {
                _targets.Remove(_target);
            }
        }
    }
    
    public void Activate()
    {
        _givenDamage = 0;
    }

    public void SetHealable(IHealable healable)
    {
        _healable = healable;
    }
    
    private void FindTargets()
    {
        _hits = Physics2D.OverlapCircleAll(
            transform.position,
            _abilityRadius,
            _targetLayer);

        _targets.Clear();

        foreach (var hit in _hits)
        {
            if (hit.TryGetComponent(out IDamageable damageable))
            {
                _targets.Add(damageable);
            }
        }
    }

    private void FindClosestTarget()
    {
        _target = null;
        float minDistance = Mathf.Infinity;

        for (var index = 0; index < _targets.Count; index++)
        {
            var target = _targets[index];
            float distanceToEnemy = Vector2.SqrMagnitude(target.Transform.position - transform.position);

            if (distanceToEnemy < minDistance)
            {
                minDistance = distanceToEnemy;
                _target = target;
            }
        }
    }
}