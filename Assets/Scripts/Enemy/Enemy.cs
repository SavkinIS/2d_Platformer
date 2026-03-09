using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Mover _mover;
    [SerializeField] private EnemyAnimator _animator;
    [SerializeField] private float _waitingTime = 2;

    private StateMachine _stateMachine;
    private Rotator _rotator;
    
    public float WaitingTime => _waitingTime;

    private void Awake()
    {
        _rotator = new Rotator(transform);
    }

    private void Start()
    {
        _stateMachine.ChangeState(typeof(IdleState));
    }

    private void Update()
    {
        _stateMachine.Tick();
    }

    private void OnEnable()
    {
        _mover.DirectionChanged += _rotator.Rotate;
    }

    private void OnDisable()
    {
        _mover.DirectionChanged -= _rotator.Rotate;
    }

    public void PlayIdle()
    {
        _animator.PlayIdle();
    }

    public void PlayMoveAnimation()
    {
        _animator.PlayMove(true);
    }

    public void Initialize(Path path)
    {
        _stateMachine = new StateMachine(this,  _mover, path);
    }
}