namespace PlayerSystem
{
    public class PlayerHurtState : IPlayerState
    {
        public bool CanMove => false;
        public bool CanJump => false;
        public bool CanAttack => false;

        private readonly PlayerAnimator _playerAnimator;
        private readonly PlayerStateMachine _stateMachine;

        public PlayerHurtState(PlayerAnimator playerAnimator, PlayerStateMachine stateMachine)
        {
            _playerAnimator = playerAnimator;
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            _playerAnimator.Hurt();
            _playerAnimator.HurtAnimationEnded += HurtAnimationEnded;
        }

        public void Update()
        {
        }

        public void Exit()
        {
            
        }

        private void HurtAnimationEnded()
        {
            _stateMachine.ChangeState(typeof(PlayerIdleState));
        }
    }
}