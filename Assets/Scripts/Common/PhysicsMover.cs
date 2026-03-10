using UnityEngine;
using UnityEngine.InputSystem;

public class PhysicsMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _jumpForce = 10f;
    
    private GroundDetector _groundDetector;
    private bool _isJumpPressed;

    public Vector2 Direction { get; private set; }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(
            Direction.x * _speed,
            _rigidbody.velocity.y);

        if (_isJumpPressed && _rigidbody.velocity.y <= 0)
        {
            _isJumpPressed = false;
        }
    }

    public void MoveDirection(InputAction.CallbackContext inputDirection)
    {
        var direction = inputDirection.ReadValue<Vector2>();
        Direction = new Vector2(direction.x, 0f);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed == false || _groundDetector.IsGrounded == false)
            return;

        _rigidbody.velocity = new Vector2(
            _rigidbody.velocity.x,
            0f);

        _rigidbody.AddForce(
            Vector2.up * _jumpForce,
            ForceMode2D.Impulse);

        _isJumpPressed = true;
    }

    public bool IsJumping()
        => _rigidbody.velocity.y > 0 && _isJumpPressed;
    
    public void SetGroundDetector(GroundDetector groundDetector)
    {
        _groundDetector = groundDetector;
    }
}