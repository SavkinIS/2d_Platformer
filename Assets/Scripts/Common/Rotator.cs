using UnityEngine;

public class Rotator
{
    private float _rotateY = 180;
    private bool _isRoteted;
    private Transform _transform;

    public Rotator(Transform transform)
    {
        _transform = transform;
    }

    public void Rotate(Vector2 direction)
    {
        if (_isRoteted == false)
            _isRoteted = true;

        if (_isRoteted)
        {
            _transform.rotation = Quaternion.Euler(Vector3.up * (direction.x < 0 ? _rotateY : 0));
        }
    }

    public void ResetRotation()
    {
        _isRoteted = false;
    }
}