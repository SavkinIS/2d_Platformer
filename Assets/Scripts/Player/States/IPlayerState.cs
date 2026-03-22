namespace PlayerSystem
{
    public interface IPlayerState
    {
        bool CanMove { get; }
        bool CanJump { get; }
        bool CanAttack { get; }
        void Enter();
        void Update();
        void Exit();
    }
}
