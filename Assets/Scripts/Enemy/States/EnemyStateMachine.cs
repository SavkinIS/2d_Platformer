using System;
using System.Collections.Generic;

namespace EnemySystem
{
    public class EnemyStateMachine : IDisposable
    {
        private IEnemyState _currentState;
        private readonly Dictionary<Type, IEnemyState> _states;
        private readonly Enemy _enemy;
        private readonly Health _health;

        public EnemyStateMachine(Enemy enemy, EnemyAnimator enemyAnimator, TargetMover targetMover, Path path,
            Health health, PlayerDetector playerDetector, Attacker attacker, Rotator rotator)
        {
            _enemy = enemy;
            _health = health;

            _states = new Dictionary<Type, IEnemyState>()
            {
                { typeof(EnemyIdleState), new EnemyIdleState(enemy, this, enemyAnimator) },
                { typeof(EnemyMoveState), new EnemyMoveState(enemyAnimator, this, targetMover, path) },
                { typeof(EnemyHurtState), new EnemyHurtState(enemyAnimator, this, health, enemy) },
                { typeof(EnemyDeadState), new EnemyDeadState(enemy, enemyAnimator, this) },
                { typeof(EnemyAttackState), new EnemyAttackState(enemy, enemyAnimator, this, attacker, rotator) },
                {
                    typeof(EnemyChaseState),
                    new EnemyChaseState(enemy, enemyAnimator, this, playerDetector, targetMover)
                },
            };
        }

        public object CurrentState => _currentState;

        public void ChangeState(Type newStateType)
        {
            if (_states.TryGetValue(newStateType, out var nextState) && nextState != _currentState)
            {
                _currentState?.Exit();
                _currentState = nextState;
                _currentState?.Enter();
            }
        }

        public void Dispose()
        {
            _currentState?.Exit();
            _states.Clear();
        }
    }
}