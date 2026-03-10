public class MoveState : IEnemyState
{
    private readonly TargetMover _targetMover;
    private readonly Path _path;
    private readonly StateMachine _stateMachine;
    private readonly EnemyAnimator _enemyAnimator;

    public MoveState(EnemyAnimator enemyAnimator, StateMachine stateMachine, TargetMover targetMover, Path path)
    {
        _enemyAnimator = enemyAnimator;
        _path = path;
        _targetMover = targetMover;
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        _enemyAnimator.PlayMove(true);
        _path.ChangePoint();
        _targetMover.SetTarget(_path.GetPoint());
        _targetMover.DestinationReached += DestinationReached;
    }

    public void Exit()
    {
        _targetMover.DestinationReached -= DestinationReached;
    }
    
    private void DestinationReached()
    {
        _stateMachine.ChangeState(typeof(IdleState));
    }
}