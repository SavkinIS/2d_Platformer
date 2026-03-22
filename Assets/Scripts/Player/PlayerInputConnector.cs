using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputConnector : IDisposable
{
    private PlayerInput _input;
    private PlayerInputBuffer _buffer;
    private readonly Rotator _rotator;

    public PlayerInputConnector(PlayerInputBuffer buffer, Transform transform)
    {
        _buffer = buffer;
        _input = new PlayerInput();
        _rotator = new Rotator(transform);
    }

    public void Enable()
    {
        _input.Enable();

        _input.Player.Move.performed += Move;
        _input.Player.Move.canceled += Move;

        _input.Player.Jump.performed += Jump;
        _input.Player.Attack_1.performed += Attack;
    }

    public void Dispose()
    {
        _input.Dispose();
    }

    public void Disable()
    {
        Dispose();
    }

    private void Move(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        _buffer.MoveInput = direction;

        _rotator.Rotate(direction);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        _buffer.Jump.Press();
    }

    private void Attack(InputAction.CallbackContext context)
    {
        _buffer.Attack.Press();
    }
}