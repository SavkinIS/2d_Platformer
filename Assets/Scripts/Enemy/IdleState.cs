using System.Collections;
using UnityEngine;

public class IdleState : IEnemyState
{
    private Enemy _enemy;
    private StateMachine _stateMachine;
    private readonly float _waitingTime;

    public IdleState(Enemy enemy, StateMachine stateMachine)
    {
        _enemy = enemy;
        _stateMachine = stateMachine;
        _waitingTime = _enemy.WaitingTime;
    }

    public void Enter()
    {
        _enemy.PlayIdle();
        _enemy.StartCoroutine(WaitCoroutine());
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

    private IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(_waitingTime);
        Execute();
    }
}