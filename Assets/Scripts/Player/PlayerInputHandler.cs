using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : IDisposable
{
    private PhysicsMover _physicsMover;
    private PlayerInput _playerInput;
    private readonly Rotator _rotator;

    public PlayerInputHandler(PhysicsMover physicsMover, Transform transform)
    {
        _physicsMover = physicsMover;
        _playerInput = new PlayerInput();
        _rotator = new Rotator(transform);
    }

    public void Subscribe()
    {
        _playerInput.Enable();
        _playerInput.Player.Move.performed += _physicsMover.MoveDirection;
        _playerInput.Player.Move.canceled += _physicsMover.MoveDirection;

        _playerInput.Player.Move.performed += Rotate;
        _playerInput.Player.Move.canceled += ResetRotete;

        _playerInput.Player.Jump.performed += _physicsMover.Jump;
    }

    public void UnSubscribe()
    {
        _playerInput.Player.Move.performed -= _physicsMover.MoveDirection;
        _playerInput.Player.Move.canceled -= _physicsMover.MoveDirection;
        
        _playerInput.Player.Move.performed -= Rotate;
        _playerInput.Player.Move.canceled -= ResetRotete;
        
        _playerInput.Player.Jump.performed -= _physicsMover.Jump;
        
        _playerInput.Disable();
    }
    
    private void Rotate(InputAction.CallbackContext performed)
    {
        _rotator.Rotate(performed.ReadValue<Vector2>());
    }

    private void ResetRotete(InputAction.CallbackContext performed)
    {
        _rotator.ResetRotation();
    }
    
    public void Dispose()
    {
        UnSubscribe();
        _playerInput?.Dispose();
    }
}