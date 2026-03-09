using System;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _arrivalThreshold = 0.25f;

    private Transform _target;
    private Rotator _rotator;
    private float _arrivalThresholdSqr;

    public event Action<Vector2> DirectionChanged;
    
    public bool IsDestinationReached { get; private set; }
    
    private bool HasTarget => _target != null;

    private bool IsCanMove => HasTarget && !IsDestinationReached && _target != null;
    
    private void Awake()
    {
        IsDestinationReached = true;
        _arrivalThresholdSqr = _arrivalThreshold * _arrivalThreshold;
    }

    private void Update()
    {
        if (IsCanMove)
        {
            Move();
        }
    }

    public void SetTarget(Transform target)
    {
        _target = target;
        IsDestinationReached = target == null;
    }

    private void Move()
    {
        Vector3 targetPosition = _target.position;
        targetPosition.y = transform.position.y;

        float step = _moveSpeed * Time.deltaTime;
        Vector3 newPos = Vector3.MoveTowards(transform.position, targetPosition, step);

        var distance = (transform.position - _target.position).sqrMagnitude;

        if (distance < _arrivalThresholdSqr)
        {
            CompleteMovement();
            return;
        }
        
        Vector3 direction = (newPos - transform.position).normalized;
        
        if (direction != Vector3.zero)
        {
            DirectionChanged?.Invoke(direction);
        }
        
        transform.position = newPos;
    }

    private void CompleteMovement()
    {
        if (_target != null)
        {
            Vector3 finalPos = _target.position;
            finalPos.y = transform.position.y;
            transform.position = finalPos;
        }

        SetTarget(null);
    }
}