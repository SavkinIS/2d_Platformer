using UnityEngine;

public class CanvasHandler : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private Player _player;

    private void Awake()
    {
        ShowGamePanel();
    }

    private void OnEnable()
    {
        _player.Dead += ShowGameOverPanel;
    }

    private void OnDisable()
    {
        _player.Dead -= ShowGameOverPanel;
    }
    
    private void ShowGameOverPanel()
    {
        _gameOverPanel.gameObject.SetActive(true);
    }
    
    private void ShowGamePanel()
    {
        _gameOverPanel.gameObject.SetActive(false);
    }
}