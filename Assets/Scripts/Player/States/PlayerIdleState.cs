namespace PlayerSystem
{
    public class PlayerIdleState : IPlayerState
    {
        private readonly PlayerAnimator _playerAnimator;

        public PlayerIdleState(PlayerAnimator playerAnimator)
        {
            _playerAnimator = playerAnimator;
        }

        public bool CanMove => true;
        public bool CanJump => true;
        public bool CanAttack => true;

        public void Enter()
        {
            _playerAnimator.Idle();
        }

        public void Update()
        {

        }

        public void Exit()
        {

        }
    }
}