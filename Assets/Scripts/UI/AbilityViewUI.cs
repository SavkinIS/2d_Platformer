using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityViewUI : MonoBehaviour
{
    [SerializeField] private Image _fillImage;
    [SerializeField] private TextMeshProUGUI _tooltipText;

    private void Awake()
    {
        _fillImage.type = Image.Type.Filled;
        _fillImage.fillMethod = Image.FillMethod.Vertical;
    }

    public void ChangeCooldown(float current, float max)
    {
        _fillImage.fillAmount = current / max;
    }

    public void Activated()
    {
        DisableTooltip();
        DisableProgress();
    }

    public void Deactivated()
    {
        EnableProgress();
    }

    public void Enabled()
    {
        EnableTooltip();
        EnableProgress();
    }

    private void EnableTooltip()
    {
        _tooltipText.gameObject.SetActive(true);
    }

    private void DisableTooltip()
    {
        _tooltipText.gameObject.SetActive(false);
    }

    private void EnableProgress()
    {
        _fillImage.gameObject.SetActive(true);
    }

    private void DisableProgress()
    {
        _fillImage.gameObject.SetActive(false);
    }
}