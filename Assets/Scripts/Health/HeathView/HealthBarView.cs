using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HealthBarView : HealthViewBase
{
    [SerializeField] protected Slider Slider;

    protected override void HealthChanged(float current, float max, float normalized)
    {
        Slider.value = normalized;
    }
}