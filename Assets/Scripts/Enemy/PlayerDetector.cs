using System;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    private const string PlayerTag = "Player";

    [SerializeField] private TriggerDetector _detector;
    private Player _player;

    public bool IsPlayerInAria { get; private set; }
    public event Action<Player> OnPlayerEnter;

    public Player Player => _player;

    public void Subscribe()
    {
        _detector.TriggerEntered += PlayerEnter;
        _detector.TriggerExited += PlayerExit;
    }

    public void Unsubscribe()
    {
        _detector.TriggerEntered -= PlayerEnter;
        _detector.TriggerExited -= PlayerExit;
    }

    protected virtual void PlayerEnter(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            Physics2D.IgnoreCollision(
                other,
                GetComponent<Collider2D>()
            );
            
            _player = player;
            IsPlayerInAria = true;

            OnPlayerEnter?.Invoke(_player);
        }
    }

    private void PlayerExit(Collider2D other)
    {
        if (TryGetComponent<Player>(out var player))
        {
            IsPlayerInAria = false;
        }
    }
}