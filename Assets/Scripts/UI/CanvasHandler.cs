using UnityEngine;
using UnityEngine.Serialization;

public class CanvasHandler : MonoBehaviour
{
    [SerializeField] private GameObject _gamePlayPanel;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private Player _player;
    [SerializeField] private AbilityViewUI vampirismViewUI;

    private void Awake()
    {
        ShowGamePanel();
    }

    private void OnEnable()
    {
        _player.Dead += ShowGameOverPanel;
        _player.AbilityHandler.Activated += vampirismViewUI.Activated;
        _player.AbilityHandler.Deactivated += vampirismViewUI.Deactivated;
        _player.AbilityHandler.Enabled += vampirismViewUI.Enabled;
        _player.AbilityHandler.CooldownChanged += vampirismViewUI.ChangeCooldown;
    }

    private void OnDisable()
    {
        _player.Dead -= ShowGameOverPanel;
        _player.AbilityHandler.Activated -= vampirismViewUI.Activated;
        _player.AbilityHandler.Deactivated -= vampirismViewUI.Deactivated;
        _player.AbilityHandler.Enabled -= vampirismViewUI.Enabled;
        _player.AbilityHandler.CooldownChanged += vampirismViewUI.ChangeCooldown;
    }
    
    private void ShowGameOverPanel()
    {
        _gameOverPanel.gameObject.SetActive(true);
    }
    
    private void ShowGamePanel()
    {
        _gamePlayPanel.SetActive(true);
        _gameOverPanel.SetActive(false);
    }
}