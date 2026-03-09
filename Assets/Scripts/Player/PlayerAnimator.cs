using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rigidbody;
    
    private PhysicsMover _physicsMover;
    private Vector2 _direction;
    private bool _jumping;
    private GroundDetector _groundDetector;

    private readonly int RunHash = Animator.StringToHash("IsRun");
    private readonly int IsGroundedHash = Animator.StringToHash("IsFall");
    private readonly int VerticalSpeedHash = Animator.StringToHash("IsJump");
    
    private void Update()
    {
        _direction = _physicsMover.Direction;
        _jumping = _physicsMover.IsJumping();
        
        if (_jumping)
        {
           Jump(_jumping);
        }

        if (_jumping == false && _groundDetector.IsGrounded == false)
        {
            Fall(_groundDetector.IsGrounded == false);
        }

        if (!_jumping && _groundDetector.IsGrounded)
        {
            Run(Mathf.Abs(_direction.x) > 0.1f);
        }
    }

    public void Initialize(PhysicsMover physicsMover, GroundDetector groundDetector)
    {
        _physicsMover =  physicsMover;
        _groundDetector = groundDetector;
    }
    
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