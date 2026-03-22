using System;
using EnemySystem;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private TargetMover _targetMover;
    [SerializeField] private EnemyAnimator _animator;
    [SerializeField] private Health _health;
    [SerializeField] private float _waitingTime = 2;
    [SerializeField] private float _attackWaitingTime = 1;
    [SerializeField] private float _playerAttackDistance = 2f;
    [SerializeField] private PlayerDetector _playerDetector;
    [SerializeField] private Attacker _attacker;

    private EnemyStateMachine _enemyStateMachine;
    private Rotator _rotator;
    private Player _player;

    public float WaitingTime => _waitingTime;
    public float AttackWaiting => _attackWaitingTime;
    public float PlayerAttackDistance => _playerAttackDistance;
    public Player Player => _player;

    public event Action<GameObject> CalledDead;

    private void Awake()
    {
        _rotator = new Rotator(transform);
    }

    private void Start()
    {
        _enemyStateMachine.ChangeState(typeof(EnemyIdleState));
    }

    private void Update()
    {
        Debug.Log(_enemyStateMachine.CurrentState);
    }

    private void OnEnable()
    {
        _playerDetector.OnPlayerEnter += PLayerInChaseZoneEntered;
        _playerDetector.Subscribe();
        _targetMover.DirectionChanged += _rotator.Rotate;
        _health.Hurted += TakeDamage;
        _health.Dead += ToDie;
    }

    private void OnDisable()
    {
        _playerDetector.OnPlayerEnter -= PLayerInChaseZoneEntered;
        _playerDetector.Unsubscribe();
        _targetMover.DirectionChanged -= _rotator.Rotate;
        _health.Hurted -= TakeDamage;
    }
    
    public void Initialize(Path path)
    {
        _enemyStateMachine = new EnemyStateMachine(this, _animator, _targetMover, path, _health, _playerDetector, _attacker, _rotator);
    }
    
    public bool IsTargetInAttackRange()
    {
        if (_player == null || _player.IsAlive == false)
            return false;

        float dist = Vector2.Distance(transform.position, _player.transform.position);
        return dist <= _playerAttackDistance;
    }
    
    public void Dead()
    {
        _enemyStateMachine.Dispose();
        CalledDead?.Invoke(gameObject);
    }
    
    private void ToDie()
    {
        _enemyStateMachine.ChangeState(typeof(EnemyDeadState));
    }
    
    private void TakeDamage()
    {
        _enemyStateMachine.ChangeState(typeof(EnemyHurtState));
    }

    private void PLayerInChaseZoneEntered(Player player)
    {
        if (player == null) 
            return;
        
        _player = player;

        if (IsTargetInAttackRange())
        {
            _enemyStateMachine.ChangeState(typeof(EnemyAttackState));
        }
        else
        {
            if (_enemyStateMachine.CurrentState != typeof(EnemyAttackState))
                _enemyStateMachine.ChangeState(typeof(EnemyChaseState));
        }
    }
}