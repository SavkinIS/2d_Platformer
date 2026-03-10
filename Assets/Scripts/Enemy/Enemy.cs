using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private TargetMover _targetMover;
    [SerializeField] private EnemyAnimator _animator;
    [SerializeField] private float _waitingTime = 2;

    private StateMachine _stateMachine;
    private Rotator _rotator;
    
    public float WaitingTime => _waitingTime;

    private void Awake()
    {
        _rotator = new Rotator(transform);
    }

    private void Start()
    {
        _stateMachine.ChangeState(typeof(IdleState));
    }

    private void OnEnable()
    {
        _targetMover.DirectionChanged += _rotator.Rotate;
    }

    private void OnDisable()
    {
        _targetMover.DirectionChanged -= _rotator.Rotate;
    }

    public void Initialize(Path path)
    {
        _stateMachine = new StateMachine(this, _animator, _targetMover, path);
    }
}