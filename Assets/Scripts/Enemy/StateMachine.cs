using System;
using System.Collections.Generic;

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