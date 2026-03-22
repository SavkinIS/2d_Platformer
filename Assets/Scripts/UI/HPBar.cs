using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Image _healthBar;
    [SerializeField] private Text _healthText;

    private readonly string _hpText = "HP : ";

    public void SetHealth(float healthCurrent, float healthMax)
    {
        _healthText.text = $"{_hpText} {healthCurrent}/{healthMax}";
        _healthBar.fillAmount = healthCurrent / healthMax;
    }
}