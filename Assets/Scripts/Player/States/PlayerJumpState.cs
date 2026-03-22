namespace PlayerSystem
{
    public class PlayerJumpState : IPlayerState
    {
        public bool CanMove => true;
        public bool CanJump => false;
        public bool CanAttack => false;

        private readonly PlayerAnimator _playerAnimator;
        private readonly PhysicsMover _physicsMover;

        public PlayerJumpState(PlayerAnimator playerAnimator, PhysicsMover physicsMover)
        {
            _playerAnimator = playerAnimator;
            _physicsMover = physicsMover;
        }

        public void Enter()
        {
            _physicsMover.JumpEnded += EndJump;
            _physicsMover.Jump();
            _playerAnimator.Jump(true);
        }

        private void EndJump()
        {
            _playerAnimator.Jump(false);
        }

        public void Update()
        {
        }

        public void Exit()
        {
            _physicsMover.JumpEnded -= EndJump;
        }
    }
}