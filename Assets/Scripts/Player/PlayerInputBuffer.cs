using UnityEngine;

public class PlayerInputBuffer
{
    private const float BufferJumpTime = 0.2f;
    private const float BufferAttackTime = 0.25f;

    public Vector2 MoveInput;
   
    public PlayerInputBuffer()
    {
        Jump = new BufferedInput(BufferJumpTime);
        Attack = new BufferedInput(BufferAttackTime);
    }
    
    public BufferedInput Jump { get; private set; }
    public BufferedInput Attack { get; private set; }

}