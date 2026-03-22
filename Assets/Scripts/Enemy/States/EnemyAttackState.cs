using System.Collections;
using EnemySystem;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{
    private Enemy _enemy;
    private EnemyAnimator _animator;
    private EnemyStateMachine _stateMachine;
    private Coroutine _attackCoroutine;
    private readonly Attacker _attacker;
    private readonly WaitForSeconds _attackWait;
    private readonly Rotator _rotator;

    public EnemyAttackState(Enemy enemy, EnemyAnimator animator, EnemyStateMachine stateMachine, Attacker attacker,
        Rotator rotator)
    {
        _enemy = enemy;
        _animator = animator;
        _stateMachine = stateMachine;
        _attacker = attacker;
        _rotator = rotator;
        
       _attackWait = new WaitForSeconds(_enemy.AttackWaiting);
    }

    public void Enter()
    {
        _animator.PlayIdle();
        _attackCoroutine = _enemy.StartCoroutine(AttackRoutine());
        _animator.AttackDealDamage += TryDealDamage;
        _animator.AttackAnimationEnded += AttackEnded;
    }

    private void AttackEnded()
    {
        if (_enemy.IsTargetInAttackRange())
        {
            _animator.PlayIdle();

            if (_attackCoroutine == null)
            {
                _attackCoroutine = _enemy.StartCoroutine(AttackRoutine());
            }
            else
            {
                _enemy.StopCoroutine(_attackCoroutine);
                _attackCoroutine = _enemy.StartCoroutine(AttackRoutine());
            }
        }
        else
        {
            _stateMachine.ChangeState(typeof(EnemyIdleState));
        }
    }

    private void TryDealDamage()
    {
        _attacker.DealDamage();
    }

    private IEnumerator AttackRoutine()
    {
        if (_enemy.Player != null)
        {
            _rotator.Rotate((_enemy.Player.transform.position - _enemy.transform.position).normalized);
        }
        
        _animator.PlayAttack();
        yield return _attackWait;
    }

    public void Exit()
    {
        if (_attackCoroutine != null)
            _enemy.StopCoroutine(_attackCoroutine);
        
        _animator.AttackDealDamage -= TryDealDamage;
        _animator.AttackAnimationEnded -= AttackEnded;
    }
}