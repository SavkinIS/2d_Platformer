using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rigidbody;

    private static readonly int RunHash = Animator.StringToHash("IsRun");
    private static readonly int IsGroundedHash = Animator.StringToHash("IsFall");
    private static readonly int VerticalSpeedHash = Animator.StringToHash("IsJump");

    public void Run(bool state)
    {
        _animator.SetBool(RunHash,state);
        _animator.SetBool(IsGroundedHash, false);
        _animator.SetBool(VerticalSpeedHash, false);
    }

    public void Jump(bool state)
    {
        _animator.SetBool(VerticalSpeedHash, state);
        
        _animator.SetBool(RunHash, false);
        _animator.SetBool(IsGroundedHash, false);
    }

    public void Fall(bool state)
    {
        _animator.SetBool(IsGroundedHash, state);
        
        _animator.SetBool(RunHash, false);
        _animator.SetBool(VerticalSpeedHash, false);
    }
}