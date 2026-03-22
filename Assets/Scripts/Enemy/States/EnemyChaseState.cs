using System.Collections;
using EnemySystem;
using UnityEngine;

public class EnemyChaseState : IEnemyState
{
    private Enemy _enemy;
    private EnemyAnimator _enemyAnimator;
    private EnemyStateMachine _stateMachine;
    private readonly PlayerDetector _visibleZoneDetector;
    private float _checkRate = 1f;
    private readonly WaitForSeconds _waitForSeconds;
    private Coroutine _playerInAriaCheckCoroutine;
    private readonly TargetMover _targetMover;
    private Coroutine _moveCoroutine;

    public EnemyChaseState(Enemy enemy, EnemyAnimator enemyAnimator, EnemyStateMachine stateMachine,
        PlayerDetector visibleZoneDetector, TargetMover targetMover)
    {
        _enemy = enemy;
        _enemyAnimator = enemyAnimator;
        _stateMachine = stateMachine;
        _visibleZoneDetector = visibleZoneDetector;
        _targetMover = targetMover;
        _waitForSeconds = new WaitForSeconds(_checkRate);
    }

    public void Enter()
    {
        _playerInAriaCheckCoroutine = _enemy.StartCoroutine(PlayerInAriaCheckCoroutine());

        if (_visibleZoneDetector.Player != null)
        {
            _targetMover.SetTarget(_visibleZoneDetector.Player.transform);

            _targetMover.EnableRun();
            _moveCoroutine = _targetMover.StartCoroutine(MoveCoroutine());
            _enemyAnimator.PlayMove(true);
        }
        else
        {
            _stateMachine.ChangeState(typeof(EnemyIdleState));
        }
    }

    private IEnumerator PlayerInAriaCheckCoroutine()
    {
        while (true)
        {
            if (_visibleZoneDetector.IsPlayerInAria == false)
            {
                _stateMachine.ChangeState(typeof(EnemyIdleState));
            }

            yield return _waitForSeconds;
        }
    }

    private IEnumerator MoveCoroutine()
    {
        while (true)
        {
            if (_enemy.IsTargetInAttackRange())
            {
                _targetMover.Stop();
                _stateMachine.ChangeState(typeof(EnemyAttackState));
                yield break;
            }
            
            _targetMover.Move();
            yield return null;
        }
    }

    public void Exit()
    {
        _targetMover.DisableRun();

        if (_playerInAriaCheckCoroutine != null)
            _enemy.StopCoroutine(_playerInAriaCheckCoroutine);

        if (_moveCoroutine != null)
            _targetMover.StopCoroutine(_moveCoroutine);
    }
}