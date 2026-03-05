using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundRadius = 0.2f;
    [SerializeField] private LayerMask _groundLayer;

    [SerializeField] private PlayerAnimator _playerAnimator;

    private Vector2 _direction;
    private bool _isGrounded;
    private float _verticalCheker;
    private bool _jumping;

    private void Update()
    {
        if (_jumping)
        {
            var verticalCurrentValue = _verticalCheker + _rigidbody.velocity.y;

            _jumping = verticalCurrentValue > _verticalCheker;
            _verticalCheker = verticalCurrentValue;

            _playerAnimator.Jump(_jumping);
        }

        if (!_jumping && !_isGrounded)
        {
            _playerAnimator.Fall(!_isGrounded);
        }

        if (!_jumping && _isGrounded)
        {
            _playerAnimator.Run(Mathf.Abs(_direction.x) > 0.1f);
        }
    }

    private void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundRadius, _groundLayer);

        _rigidbody.velocity = new Vector2(
            _direction.x * _speed,
            _rigidbody.velocity.y);

        if (!_isGrounded && _rigidbody.velocity.y < 0.01f)
        {
            _rigidbody.velocity += Vector2.down * 0.1f;
        }
    }

    public void MoveDirection(InputAction.CallbackContext inputDirection)
    {
        var direction = inputDirection.ReadValue<Vector2>();
        _direction = new Vector2(direction.x, 0f);

        _spriteRenderer.flipX = _direction.x < 0;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!context.performed || !_isGrounded)
            return;

        _rigidbody.velocity = new Vector2(
            _rigidbody.velocity.x,
            0f);

        _rigidbody.AddForce(
            Vector2.up * _jumpForce,
            ForceMode2D.Impulse);

        _jumping = true;
        _verticalCheker = 0;
    }
}