using UnityEngine;

public class PlayerRoot : MonoBehaviour
{ 
    [SerializeField] private PhysicsMover _physicsMover;
    [SerializeField] private Collector _collector;
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private GroundDetector _groundDetector;
    
    private PlayerInputHandler _playerHandler;
    private Wallet _wallet;
    
    private void Awake()
    {
        _wallet =  new Wallet();
        _playerHandler = new PlayerInputHandler(_physicsMover, transform);
        _playerAnimator.Initialize(_physicsMover, _groundDetector);
        _physicsMover.SetGroundDetector(_groundDetector);
    }
    
    private void OnEnable()
    {
        _playerHandler.Subscribe();
        _collector.GemColleted += _wallet.IncrementGemCount;
    }

    private void OnDisable()
    {
        _playerHandler.UnSubscribe();
        _collector.GemColleted -= _wallet.IncrementGemCount;
    }
}