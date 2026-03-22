namespace PlayerSystem
{
    public class PlayerMovementState : IPlayerState
    {
        public bool CanMove => true;
        public bool CanJump => true;
        public bool CanAttack => false;

        private readonly PlayerAnimator _playerAnimator;
        private readonly PlayerInputBuffer _inputBuffer;

        public PlayerMovementState(PlayerAnimator playerAnimator,
            PlayerInputBuffer inputBuffer)
        {
            _playerAnimator = playerAnimator;
            _inputBuffer = inputBuffer;
            
        }

        public void Enter()
        {
            _playerAnimator.Run(true);
        }

        public void Update()
        {
        }

        public void Exit()
        {

        }
    }
}