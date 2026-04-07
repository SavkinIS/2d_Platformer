using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PLayerInputHandler : IDisposable
{
    private PhysicsMover _physicsMover;
    private PlayerInput _playerInput;
    private readonly Rotator _rotator;
    private readonly Player _player;

    public PLayerInputHandler(PhysicsMover physicsMover, Player player, Transform rotateTransform)
    {
        _physicsMover = physicsMover;
        _playerInput = new PlayerInput();
        _rotator = new Rotator(rotateTransform);
        _player = player;
    }

    public void Subscribe()
    {
        _playerInput.Enable();
        _playerInput.Player.Move.performed += Move;
        _playerInput.Player.Move.canceled += Move;

        _playerInput.Player.Move.performed += Rotate;

        _playerInput.Player.Jump.performed += Jump;

        _playerInput.Player.Attack_1.performed += Attack;
        _playerInput.Player.Ability.performed += ActivateAbility;
    }

    public void UnSubscribe()
    {
        _playerInput.Player.Move.performed -= Move;
        _playerInput.Player.Move.canceled -= Move;

        _playerInput.Player.Move.performed -= Rotate;

        _playerInput.Player.Jump.performed -= Jump;

        _playerInput.Player.Attack_1.performed -= Attack;
        _playerInput.Player.Ability.performed -= ActivateAbility;

        _playerInput.Disable();
    }

    private void ActivateAbility(InputAction.CallbackContext obj)
    {
        _player.Vampirism.Activate();
    }

    private void Attack(InputAction.CallbackContext obj)
    {
        _player.Attack();
    }

    private void Jump(InputAction.CallbackContext context)
    {
        _physicsMover.Jump();
    }

    private void Move(InputAction.CallbackContext performed)
    {
        if (performed.canceled)
            _physicsMover.MoveDirection(Vector2.zero);
        else
            _physicsMover.MoveDirection(performed.ReadValue<Vector2>());
    }

    private void Rotate(InputAction.CallbackContext performed)
    {
        _rotator.Rotate(performed.ReadValue<Vector2>());
    }

    public void Dispose()
    {
        UnSubscribe();
        _playerInput?.Dispose();
    }
}