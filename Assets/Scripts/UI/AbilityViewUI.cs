using System;
using UnityEngine;
using UnityEngine.UI;

public class AbilityViewUI : MonoBehaviour
{
    [SerializeField] private Image _fillImage;
    [SerializeField] private GameObject _textGo;
    
    private void Awake()
    {
        _fillImage.type = Image.Type.Filled;
        _fillImage.fillMethod = Image.FillMethod.Vertical;
    }

    public void ChangeCooldown(float current, float max)
    {
        _fillImage.fillAmount = current / max;
    }
    
    public void EnableTooltip()
    {
        _textGo.SetActive(true);
    }

    public void DisableTooltip()
    {
        _textGo.SetActive(false);
    }
    
    public void EnableProgress()
    {
        _fillImage.gameObject.SetActive(true);
    }
    
    public void DisableProgress()
    {
        _fillImage.gameObject.SetActive(false);
    }
}