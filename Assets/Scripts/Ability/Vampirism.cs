using System;
using System.Collections.Generic;
using UnityEngine;


public class Vampirism : MonoBehaviour
{
    private const int HalfReduce = 2;

    [SerializeField] private TriggerDetector _detector;
    [SerializeField] private SpriteRenderer _effect;
    [SerializeField] private float _damagePerSecond = 10;
    [SerializeField] private LayerMask _targetLayer;

    private List<IDamageable> _targets = new List<IDamageable>();
    private IDamageable _target;
    private Collider2D[] _hits;
    private IHealable _healable;
    private float _givenDamage;

    private void Awake()
    {
        _effect.enabled = false;
    }

    private void OnEnable()
    {
        _detector.TriggerEntered += AddTarget;
        _detector.TriggerExited += RemoveTarget;
    }

    private void OnDisable()
    {
        _detector.TriggerEntered -= AddTarget;
        _detector.TriggerExited -= RemoveTarget;
    }

    public void Execute(float delta)
    {
        FindClosestTarget();
        
        if (_target != null)
        {
            _target.TakeDamage(_damagePerSecond * delta, false);
            _healable.Heal(_damagePerSecond * delta);

            if (_target.IsAlive == false)
            {
                _targets.Remove(_target);
            }
        }
    }
    
    public void Activate()
    {
        _givenDamage = 0;
        _effect.enabled = true;
    }

    public void Deactivate()
    {
        _effect.enabled = false;
    }

    public void SetHealable(IHealable healable)
    {
        _healable = healable;
    }
    
    public void FindTargets()
    {
        _hits = Physics2D.OverlapCircleAll(
            transform.position,
            _effect.transform.lossyScale.x / HalfReduce,
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
    
    private void AddTarget(Collider2D collider)
    {
        FindTargets();
    }

    private void RemoveTarget(Collider2D collider)
    {
        FindTargets();
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