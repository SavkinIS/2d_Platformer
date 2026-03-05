using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] private List<Transform> _points;

    private int _currentPathPoint = 0;
    private bool _isForwardDirection = true;

    private void Awake()
    {
        foreach (Transform point in _points)
            point.gameObject.SetActive(false);
    }

    public Transform GetPoint()
    {
        return _points[_currentPathPoint];
    }

    public void ChangePoint()
    {
        if (_isForwardDirection)
            _currentPathPoint = (++_currentPathPoint) % _points.Count;
        else
            _currentPathPoint = (--_currentPathPoint + _points.Count) % _points.Count;

        if (_currentPathPoint == 0)
        {
            _isForwardDirection = true;
        }
        else if (_currentPathPoint == _points.Count - 1)
        {
            _isForwardDirection = false;
        }
    }
}