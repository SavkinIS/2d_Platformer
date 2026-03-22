using System.Collections;
using UnityEngine;

namespace EnemySystem
{
    public class EnemyIdleState : IEnemyState
    {
        private Enemy _enemy;
        private EnemyStateMachine _enemyStateMachine;
        private readonly float _waitingTime;
        private readonly EnemyAnimator _enemyAnimator;

        public EnemyIdleState(Enemy enemy, EnemyStateMachine enemyStateMachine, EnemyAnimator enemyAnimator)
        {
            _enemy = enemy;
            _enemyStateMachine = enemyStateMachine;
            _waitingTime = _enemy.WaitingTime;
            _enemyAnimator = enemyAnimator;
        }

        public void Enter()
        {
            _enemyAnimator.PlayIdle();
            _enemy.StartCoroutine(WaitCoroutine());
        }

        public void Exit()
        {

        }

        private IEnumerator WaitCoroutine()
        {
            yield return new WaitForSeconds(_waitingTime);
            Execute();
        }

        private void Execute()
        {
            _enemyStateMachine.ChangeState(typeof(EnemyMoveState));
        }
    }
}