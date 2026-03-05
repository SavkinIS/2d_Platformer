using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private IEnemyState _currentState;

    private readonly Dictionary<Type, IEnemyState> _states;

    public StateMachine(Enemy enemy, Mover mover, Path path)
    {
        _states = new Dictionary<Type, IEnemyState>()
        {
            { typeof(IdleState), new IdleState(enemy, this) },
            { typeof(MoveState), new MoveState(enemy, this, mover, path) }
        };
    }

    public void ChangeState(Type newStateType)
    {
        if (_states.TryGetValue(newStateType, out var nextState) && nextState != _currentState)
        {
            _currentState?.Exit();
            _currentState = nextState;
            _currentState?.Enter();
        }
    }

    public void Tick()
    {
        _currentState.Tick();
    }
}

public interface IEnemyState
{
    void Enter();
    void Exit();
    void Tick();
}

public class IdleState : IEnemyState
{
    private Enemy _enemy;
    private StateMachine _stateMachine;
    private readonly float _waitingTime;

    public IdleState(Enemy enemy, StateMachine stateMachine)
    {
        _enemy = enemy;
        _stateMachine = stateMachine;
        _waitingTime = _enemy.WaitngTime;
    }

    public void Enter()
    {
        _enemy.PlayIdle();
        _enemy.StartCoroutine(WaitCourutine());
    }

    public void Tick()
    {
    }

    private void Execute()
    {
        _stateMachine.ChangeState(typeof(MoveState));
    }

    public void Exit()
    {
    }

    private IEnumerator WaitCourutine()
    {
        yield return new WaitForSeconds(_waitingTime);
        Execute();
    }
}

public class MoveState : IEnemyState
{
    private Enemy _enemy;
    private readonly Mover _mover;
    private readonly Path _path;
    private readonly StateMachine _stateMachine;


    public MoveState(Enemy enemy, StateMachine stateMachine, Mover mover, Path path)
    {
        _enemy = enemy;
        _path = path;
        _mover = mover;
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        _enemy.PlayMoveAnimation();
        _path.ChangePoint();
        _mover.SetTarget(_path.GetPoint());
    }

    public void Tick()
    {
        if (_mover.IsDestinationReached)
        {
            _stateMachine.ChangeState(typeof(IdleState));
        }
            
    }

    public void Exit()
    {
        
    }
}