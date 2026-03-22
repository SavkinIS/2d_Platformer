namespace PlayerSystem
{
    public class DeathState : IPlayerState
    {
        public bool CanMove => false;
        public bool CanJump => false;
        public bool CanAttack => false;

        private readonly PlayerAnimator _playerAnimator;

        public DeathState(PlayerAnimator playerAnimator)
        {
            _playerAnimator = playerAnimator;
        }

        public void Enter()
        {
            _playerAnimator.Dead();
        }

        public void Update()
        {
        }

        public void Exit()
        {
        }
    }
}