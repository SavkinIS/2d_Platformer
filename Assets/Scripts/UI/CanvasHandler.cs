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
        _player.SetAbilityView(vampirismViewUI);
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
        _gamePlayPanel.SetActive(true);
        _gameOverPanel.SetActive(false);
    }
}