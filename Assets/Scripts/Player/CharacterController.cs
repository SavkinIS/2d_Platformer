using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    
    private PlayerInput _playerInput;
    
    private void Awake()
    {
        _playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
        _playerInput.Player.Move.performed += _playerMovement.MoveDirection;
        _playerInput.Player.Move.canceled += _playerMovement.MoveDirection;

        _playerInput.Player.Jump.performed += _playerMovement.Jump;
    }

    private void OnDisable()
    {
        _playerInput.Player.Move.performed -= _playerMovement.MoveDirection;
        _playerInput.Player.Move.canceled -= _playerMovement.MoveDirection;
        
        _playerInput.Player.Jump.performed -= _playerMovement.Jump;
        
        _playerInput.Disable();
    }
}