using System;
using PlayerSystem;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PhysicsMover _physicsMover;
    [SerializeField] private Collector _collector;
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private Health _health;
    [SerializeField] private Attacker _attacker;

    private PlayerInputConnector _inputConnector;
    private Wallet _wallet;
    private PlayerInputBuffer _inputBuffer;
    private PlayerStateMachine _stateMachine;

    public bool IsAlive => _health.IsAlive;
    public event Action<float, float> HealthChanged;
    public event Action Dead;

    private void Awake()
    {
        _wallet = new Wallet();
        _inputBuffer = new PlayerInputBuffer();
        _inputConnector = new PlayerInputConnector(_inputBuffer, transform);
        _physicsMover.SetGroundDetector(_groundDetector);

        _stateMachine = new PlayerStateMachine(_playerAnimator, _physicsMover, _inputBuffer, _attacker);
        _stateMachine.ChangeState(typeof(PlayerIdleState));
    }

    private void Start()
    {
        HealthChanged?.Invoke(_health.CurrentHealth, _health.MaxHealth);
    }

    private void Update()
    {
        ProcessInput();

        _stateMachine.Update();
    }

    private void OnEnable()
    {
        _inputConnector.Enable();
        _collector.GemColleted += _wallet.IncrementGemCount;
        _collector.AidKitColleted += _health.AidKitCollected;
        _health.Dead += ToDie;
        _health.Hurted += Hurted;
        _health.HealthRestored += HealthRestored;
    }

    private void OnDisable()
    {
        _inputConnector.Disable();
        _collector.GemColleted -= _wallet.IncrementGemCount;
        _health.Dead -= ToDie;
        _health.HealthRestored -= HealthRestored;
    }

    private void ProcessInput()
    {
        HandleMove();
        HandleFall();
        HandleJump();

        HandleAttack();
    }

    private void HandleFall()
    {
        var jumping = _physicsMover.IsJumping();

        if (jumping == false && _groundDetector.IsGrounded == false)
        {
            if (_stateMachine.CurrentState is not PlayerFallState)
            {
                _stateMachine.ChangeState(typeof(PlayerFallState));
            }
        }
        else if (_stateMachine.CurrentState is PlayerFallState && _groundDetector.IsGrounded)
        {
            _stateMachine.ChangeState(typeof(PlayerIdleState));
        }
    }

    private void HandleMove()
    {
        if (_stateMachine.CurrentState.CanMove)
            _physicsMover.MoveDirection(_inputBuffer.MoveInput);

        if (_stateMachine.IsCurrentStateCanMove && _groundDetector.IsGrounded)
        {
            if (_inputBuffer.MoveInput == Vector2.zero)
            {
                if (_stateMachine.CurrentState is PlayerMovementState)
                {
                    _stateMachine.ChangeState(typeof(PlayerIdleState));
                }

                return;
            }

            if (_stateMachine.CurrentState is PlayerIdleState)
            {
                _stateMachine.ChangeState(typeof(PlayerMovementState));
            }
        }
    }

    private void HandleJump()
    {
        if (_stateMachine.IsCurrentStateCanJump && _inputBuffer.Jump.TryConsume() && _groundDetector.IsGrounded)
        {
            _stateMachine.ChangeState(typeof(PlayerJumpState));
        }

        if (_stateMachine.CurrentState is PlayerJumpState && _physicsMover.IsJumping() == false &&
            _groundDetector.IsGrounded)
        {
            _stateMachine.ChangeState(typeof(PlayerIdleState));
        }
    }

    private void HandleAttack()
    {
        if (_stateMachine.IsCurrentStateCanAttack && _inputBuffer.Attack.TryConsume())
        {
            _stateMachine.ChangeState(typeof(PlayerAttackState));
        }
    }

    private void ToDie()
    {
        _playerAnimator.Dead();
        _inputConnector.Disable();
        Dead?.Invoke();
    }

    private void Hurted()
    {
        _playerAnimator.Hurt();
        HealthChanged?.Invoke(_health.CurrentHealth, _health.MaxHealth);
    }

    private void HealthRestored()
    {
        HealthChanged?.Invoke(_health.CurrentHealth, _health.MaxHealth);
    }
}