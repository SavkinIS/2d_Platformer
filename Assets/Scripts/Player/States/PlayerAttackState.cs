namespace PlayerSystem
{
    public class PlayerAttackState : IPlayerState
    {
        public bool CanMove => false;
        public bool CanJump => false;
        public bool CanAttack { get; private set; }

        private readonly PlayerAnimator _playerAnimator;
        private readonly Attacker _attacker;
        private readonly PlayerStateMachine _playerStateMachine;

        public PlayerAttackState(PlayerStateMachine playerStateMachine, PlayerAnimator playerAnimator,
            Attacker attacker)
        {
            _playerAnimator = playerAnimator;
            _attacker = attacker;
            _playerStateMachine = playerStateMachine;
        }

        public void Enter()
        {
            CanAttack = false;
            _playerAnimator.AttackDealDamage += _attacker.DealDamage;
            _playerAnimator.AttackEnded += AttackEnded;
            _playerAnimator.Attack();
        }

        private void AttackEnded()
        {
            CanAttack = true;
            _playerStateMachine?.ChangeState(typeof(PlayerIdleState));
        }

        public void Update()
        {
        }

        public void Exit()
        {
            _playerAnimator.AttackDealDamage -= _attacker.DealDamage;
            _playerAnimator.AttackEnded -= AttackEnded;

            CanAttack = true;
        }
    }
}