public class MoveState : IEnemyState
{
    private Enemy _enemy;
    private readonly Mover _mover;
    private readonly Path _path;
    private readonly StateMachine _stateMachine;

    public MoveState(Enemy enemy, StateMachine stateMachine, Mover mover, Path path)
    {
        _enemy = enemy;
        _path = path;
        _mover = mover;
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        _enemy.PlayMoveAnimation();
        _path.ChangePoint();
        _mover.SetTarget(_path.GetPoint());
    }

    public void Tick()
    {
        if (_mover.IsDestinationReached)
        {
            _stateMachine.ChangeState(typeof(IdleState));
        }
    }

    public void Exit()
    {
        
    }
}