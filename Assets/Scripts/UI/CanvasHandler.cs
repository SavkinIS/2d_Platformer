using UnityEngine;

public class CanvasHandler : MonoBehaviour
{
    [SerializeField] private GameUIHandler _gamePanel;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private Player _player;

    private void Awake()
    {
        ShowGamePanel();
    }

    private void OnEnable()
    {
        _player.HealthChanged += _gamePanel.HPBar.SetHealth;
        _player.Dead += ShowGameOverPanel;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= _gamePanel.HPBar.SetHealth;
        _player.Dead -= ShowGameOverPanel;
    }
    
    private void ShowGameOverPanel()
    {
        _gamePanel.gameObject.SetActive(false);
        _gameOverPanel.gameObject.SetActive(true);
    }
    
    private void ShowGamePanel()
    {
        _gamePanel.gameObject.SetActive(true);
        _gameOverPanel.gameObject.SetActive(false);
    }
}