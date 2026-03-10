using System;
using System.Collections.Generic;

public class StateMachine
{
    private IEnemyState _currentState;
    private readonly Dictionary<Type, IEnemyState> _states;

    public StateMachine(Enemy enemy, EnemyAnimator enemyAnimator, TargetMover targetMover, Path path)
    {
        _states = new Dictionary<Type, IEnemyState>()
        {
            { typeof(IdleState), new IdleState(enemy, this, enemyAnimator) },
            { typeof(MoveState), new MoveState(enemyAnimator, this, targetMover, path) }
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
}