using UnityEngine;

public class BufferedInput
{
    private readonly float _bufferTime;
    
    private float _pressedTime;
    private bool _isBuffered;
    
    public BufferedInput(float bufferTime)
    {
        _bufferTime = bufferTime;
    }

    public void Press()
    {
        _pressedTime = Time.time;
        _isBuffered = true;
    }

    public bool TryConsume()
    {
        if (_isBuffered == false)
            return false;

        if (Time.time - _pressedTime > _bufferTime)
        {
            _isBuffered = false;
            return false;
        }

        _isBuffered = false;
        return true;
    }

    public bool IsBuffered()
    {
        if (_isBuffered == false)
            return false;
        
        if (Time.time - _pressedTime > _bufferTime)
        {
            _isBuffered = false;
            return false;
        }

        return true;
    }
}