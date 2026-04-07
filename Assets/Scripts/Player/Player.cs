using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PhysicsMover _physicsMover;
    [SerializeField] private Collector _collector;
    [SerializeField] private PlayerAnimatorBase _playerAnimator;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private Health _health;
    [SerializeField] private Attacker _attacker;
    [SerializeField] private Transform _rotateTransform;
    [SerializeField] private HealthViewBase _healthBar;
    [SerializeField] private Vampirism _vampirism;

    private PLayerInputHandler _inputHandler;
    private Wallet _wallet;
    private PlayerState _playerState;

    private readonly (bool IsCanMove, bool IsCanJump, bool IsCanAttack) _allBlockState = (false, false, false);
    private readonly (bool IsCanMove, bool IsCanJump, bool IsCanAttack) _idleState = (true, true, true);
    private readonly (bool IsCanMove, bool IsCanJump, bool IsCanAttack) _jumpsState = (true, true, false);

    public bool IsAlive => _health.IsAlive;
    public Vampirism Vampirism => _vampirism;
    public event Action Dead;

    private void Awake()
    {
        _playerState = new PlayerState();
        _physicsMover.SetPlayerState(_playerState);
        _wallet = new Wallet();
        _inputHandler = new PLayerInputHandler(_physicsMover, this, _rotateTransform);
        _physicsMover.SetGroundDetector(_groundDetector);
        _vampirism.SetHealable(_health);
    }

    private void Start()
    {
        _health.Refresh();
    }

    private void Update()
    {
        var direction = _physicsMover.Direction;
        var jumping = _physicsMover.IsJumping();

        if (jumping && _playerState.IsCanJump)
        {
            _playerAnimator.Jump(jumping);
            _playerState.SetState(_jumpsState);
        }

        if (jumping == false && _groundDetector.IsGrounded == false)
        {
            _playerAnimator.Fall(_groundDetector.IsGrounded == false);
            _playerState.SetState(_jumpsState);
        }

        if (!jumping && _groundDetector.IsGrounded && _playerState.IsCanMove)
        {
            _playerState.SetState(_idleState);
            _playerAnimator.Run(Mathf.Abs(direction.x) > 0.1f);
        }
    }

    private void OnEnable()
    {
        _inputHandler.Subscribe();
        _collector.GemColleted += _wallet.IncrementGemCount;
        _collector.AidKitColleted += _health.AidKitCollected;
        _health.Dead += ToDie;
        _health.Hurted += Hurted;
        _health.HealthRestored += HealthRestored;
        _health.Changed += _healthBar.HealthChangedInternal;
        _playerAnimator.AttackDealDamage += _attacker.DealDamage;
        _playerAnimator.AttackEnded += AttackEnded;
        _playerAnimator.HurtAnimationEnded += HurtEnded;
    }

    private void AttackEnded()
    {
        _playerState.SetState(_idleState);
    }

    private void OnDisable()
    {
        _inputHandler.UnSubscribe();
        _collector.GemColleted -= _wallet.IncrementGemCount;
        _collector.AidKitColleted -= _health.AidKitCollected;
        _health.Dead -= ToDie;
        _health.Hurted -= Hurted;
        _health.HealthRestored -= HealthRestored;
        _health.Changed -= _healthBar.HealthChangedInternal;
        _playerAnimator.AttackDealDamage += _attacker.DealDamage;
    }

    public void Attack()
    {
        if (_playerState.IsCanAttack == false)
            return;

        _playerAnimator.Attack();
        _playerState.SetState(_allBlockState);
    }

    private void ToDie()
    {
        _playerState.SetState(_allBlockState);
        _playerAnimator.Dead();
        _inputHandler.Dispose();
        Dead?.Invoke();
    }

    private void Hurted()
    {
        _playerState.SetState(_allBlockState);
        _playerAnimator.Hurt();
    }

    private void HealthRestored()
    {
        _playerState.SetState(_allBlockState);
    }

    private void HurtEnded()
    {
        _playerState.SetState(_idleState);
    }
}