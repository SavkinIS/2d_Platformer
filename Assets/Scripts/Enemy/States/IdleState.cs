using System.Collections;
using UnityEngine;

public class IdleState : IEnemyState
{
    private Enemy _enemy;
    private StateMachine _stateMachine;
    private readonly float _waitingTime;
    private readonly EnemyAnimator _enemyAnimator;

    public IdleState(Enemy enemy, StateMachine stateMachine, EnemyAnimator enemyAnimator)
    {
        _enemy = enemy;
        _stateMachine = stateMachine;
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
        _stateMachine.ChangeState(typeof(MoveState));
    }
}