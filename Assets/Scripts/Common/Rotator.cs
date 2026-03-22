using UnityEngine;

public class Rotator
{
    private float _rotateY = 180;
    private Transform _transform;
    private readonly Quaternion _leftAngles;
    private readonly Quaternion _rightAngles;

    public Rotator(Transform transform)
    {
        _transform = transform;
        _leftAngles = Quaternion.Euler(Vector3.up * _rotateY);
        _rightAngles = Quaternion.Euler(Vector3.up * 0);
    }

    public void Rotate(Vector2 direction)
    {
        if (Vector2.zero == direction || Mathf.Approximately(direction.x, Vector2.zero.x))
            return; 
                
        _transform.rotation = direction.x < 0 ? _leftAngles : _rightAngles;
    }
}