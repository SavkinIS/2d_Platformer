using System.Collections;
using UnityEngine;

namespace EnemySystem
{
    public class EnemyMoveState : IEnemyState
    {
        private readonly TargetMover _targetMover;
        private readonly Path _path;
        private readonly EnemyStateMachine _enemyStateMachine;
        private readonly EnemyAnimator _enemyAnimator;
        private Coroutine _moveCoroutine;

        public EnemyMoveState(EnemyAnimator enemyAnimator, EnemyStateMachine enemyStateMachine,
            TargetMover targetMover, Path path)
        {
            _enemyAnimator = enemyAnimator;
            _path = path;
            _targetMover = targetMover;
            _enemyStateMachine = enemyStateMachine;
        }

        public void Enter()
        {
            _enemyAnimator.PlayMove(true);
            _targetMover.SetTarget(_path.GetPoint());
            _targetMover.DestinationReached += DestinationReached;
            _moveCoroutine = _targetMover.StartCoroutine(MoveCoroutine());
        }

        private IEnumerator MoveCoroutine()
        {
            while (true)
            {
                _targetMover.Move();
                yield return null;
            }
        }

        public void Exit()
        {
            if (_moveCoroutine  != null)
                _targetMover.StopCoroutine(_moveCoroutine);
            
            _targetMover.DestinationReached -= DestinationReached;
        }

        private void DestinationReached()
        {
            _enemyStateMachine.ChangeState(typeof(EnemyIdleState));
            _path.ChangePoint();
        }
    }
}