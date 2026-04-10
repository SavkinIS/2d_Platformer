using System;
using System.Collections;
using UnityEngine;

public class HealthSmoothBarView : HealthBarView
{
    [SerializeField] private float _speed = 0.3f;
    [SerializeField] private Health _health;

    private float _targetValue;
    private Coroutine _fillCoroutine;
    
    private void OnEnable()
    {
        _health.Changed += HealthChangedInternal;
    }

    private void OnDisable()
    {
        _health.Changed -= HealthChangedInternal;
    }

    protected override void HealthChanged(float current, float max, float normalized)
    {
        _targetValue = normalized;

        if (_fillCoroutine != null)
            StopCoroutine(_fillCoroutine);

        _fillCoroutine = StartCoroutine(FillCoroutine());
    }

    private IEnumerator FillCoroutine()
    {
        while (!Mathf.Approximately(Slider.value, _targetValue))
        {
            Slider.value = Mathf.Lerp(
                Slider.value,
                _targetValue,
                _speed * Time.deltaTime
            );

            yield return null;
        }

        Slider.value = _targetValue;
    }
}