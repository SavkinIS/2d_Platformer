using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityState
{
    Active,
    Waiting,
    Cooldown
}

public class Vampirism : MonoBehaviour
{
    private const int SecondStep = 1;
    private const int HalfReduce = 2;

    [SerializeField] private float _damagePerSecond = 2f;
    [SerializeField] private TriggerDetector _detector;
    [SerializeField] private SpriteRenderer _effect;
    [SerializeField] private float _activeTime = 6f;
    [SerializeField] private float _cooldownTime = 4f;
    [SerializeField] private LayerMask _targetLayer;

    private IHealable _healable;
    private WaitForSeconds _waitingOneSecond;
    private AbilityState _currentState = AbilityState.Waiting;
    private List<IDamageable> _targets = new List<IDamageable>();
    private IDamageable _target;

    public event Action<AbilityState> StateChanged;
    public event Action<float, float> CooldownChanged;

    private void Awake()
    {
        _waitingOneSecond = new WaitForSeconds(SecondStep);
        _effect.enabled = false;
    }

    private void OnEnable()
    {
        _detector.TriggerEntered += AddTarget;
        _detector.TriggerEntered += RemoveTarget;
    }

    private void OnDisable()
    {
        _detector.TriggerEntered -= AddTarget;
        _detector.TriggerEntered -= RemoveTarget;
    }

    public void SetHealable(IHealable healable)
    {
        _healable = healable;
    }

    public void Activate()
    {
        if (_currentState != AbilityState.Waiting)
            return;

        _currentState = AbilityState.Active;
        _effect.enabled = true;
        StateChanged?.Invoke(_currentState);
        FindTargets();
        StartCoroutine(StartExecuteCoroutine());
    }

    private IEnumerator StartExecuteCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < _activeTime)
        {
            FindClosestTarget();
            Execute();

            yield return _waitingOneSecond;

            elapsedTime += SecondStep;
        }

        Deactivate();
    }

    private IEnumerator TimerCoroutine()
    {
        float elapsedTime = 0;
        float timeTick = 0;

        while (elapsedTime <= _cooldownTime)
        {
            elapsedTime += Time.deltaTime;
            CooldownChanged?.Invoke(elapsedTime, _cooldownTime);
            yield return null;
        }

        Enable();
    }

    private void Enable()
    {
        if (_currentState != AbilityState.Cooldown)
            return;

        _currentState = AbilityState.Waiting;
        StateChanged?.Invoke(_currentState);
    }

    private void Deactivate()
    {
        if (_currentState != AbilityState.Active)
            return;

        CooldownChanged?.Invoke(0, _cooldownTime);
        _currentState = AbilityState.Cooldown;
        _effect.enabled = false;
        StateChanged?.Invoke(_currentState);
        StartCoroutine(TimerCoroutine());
    }

    private void Execute()
    {
        if (_target != null)
        {
            _target.TakeDamage(_damagePerSecond, false);
            _healable.Heal(_damagePerSecond);
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

    private void FindTargets()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            _effect.transform.lossyScale.x / HalfReduce,
            _targetLayer);

        
        _targets.Clear();

        foreach (var hit in hits)
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