using System;

public interface IHealth
{
    event Action<float, float, float> Changed;
    void Refresh(float changedValue = 0);
}