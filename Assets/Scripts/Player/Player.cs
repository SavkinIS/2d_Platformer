using System;
using UnityEngine;

public class Player : MonoBehaviour
{ 
    [SerializeField] private PhysicsMover _physicsMover;
    [SerializeField] private Collector _collector;
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private GroundDetector _groundDetector;
    
    private PlayerInputConnector _inputConnector;
    private Wallet _wallet;
    
    private void Awake()
    {
        _wallet =  new Wallet();
        _inputConnector = new PlayerInputConnector(_physicsMover, transform);
        _physicsMover.SetGroundDetector(_groundDetector);
    }

    private void Update()
    {
        var direction = _physicsMover.Direction;
        var jumping = _physicsMover.IsJumping();
        
        if (jumping)
        {
            _playerAnimator.Jump(jumping);
        }

        if (jumping == false && _groundDetector.IsGrounded == false)
        {
            _playerAnimator.Fall(_groundDetector.IsGrounded == false);
        }

        if (!jumping && _groundDetector.IsGrounded)
        {
            _playerAnimator.Run(Mathf.Abs(direction.x) > 0.1f);
        }
    }

    private void OnEnable()
    {
        _inputConnector.Subscribe();
        _collector.GemColleted += _wallet.IncrementGemCount;
    }

    private void OnDisable()
    {
        _inputConnector.UnSubscribe();
        _collector.GemColleted -= _wallet.IncrementGemCount;
    }
}