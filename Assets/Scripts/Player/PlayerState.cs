public class PlayerState
{
    public bool IsCanMove { get; private set; } = true;
    public bool IsCanJump { get; private set; } = true;
    public bool IsCanAttack { get; private set; } =  true;

    public void SetState((bool IsCanMove, bool IsCanJump, bool IsCanAttack) state)
    {
        IsCanMove = state.IsCanMove;
        IsCanJump = state.IsCanJump;
        IsCanAttack = state.IsCanAttack;
    }
}