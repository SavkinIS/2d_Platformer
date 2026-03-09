using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PhysicsMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _jumpForce = 10f;
    
    private GroundDetector _groundDetector;
    private bool _jumping;
    private float _verticalChecker;

    public Vector2 Direction { get; private set; }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(
            Direction.x * _speed,
            _rigidbody.velocity.y);
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

        _jumping = true;
        _verticalChecker = 0;
    }

    public bool IsJumping()
    {
        var verticalCurrentValue = _verticalChecker + _rigidbody.velocity.y;

        _jumping = verticalCurrentValue > _verticalChecker;
        _verticalChecker = verticalCurrentValue;

        return _jumping;
    }
    
    public void SetGroundDetector(GroundDetector groundDetector)
    {
        _groundDetector = groundDetector;
    }
}