using UnityEngine;

public abstract class HealthViewBase : MonoBehaviour
{
    public void HealthChangedInternal(float current, float max, float changedValue)
    {
        float normalized = current <= 0 ? 0 : current / max;
        HealthChanged(current, max, normalized);
    }

    protected abstract void HealthChanged(float current, float max, float normalized);
}