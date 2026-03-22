using UnityEngine;

public class GameUIHandler : MonoBehaviour
{
    [SerializeField] private HPBar _healthBar;
    
    public HPBar HPBar => _healthBar;
}