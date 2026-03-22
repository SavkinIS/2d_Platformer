namespace PlayerSystem
{
    public class PlayerFallState : IPlayerState
    {
        public bool CanMove => true;
        public bool CanJump => false;
        public bool CanAttack => false;

        private readonly PlayerAnimator _playerAnimator;
        private readonly PhysicsMover _physicsMover;

        public PlayerFallState(PlayerAnimator playerAnimator, PhysicsMover physicsMover)
        {
            _playerAnimator = playerAnimator;
            _physicsMover = physicsMover;
        }

        public void Enter()
        {
            _playerAnimator.Fall(true);
        }

        public void Update()
        {

        }

        public void Exit()
        {

        }
    }
}