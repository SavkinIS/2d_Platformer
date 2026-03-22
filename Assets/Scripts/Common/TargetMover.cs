using System;
using UnityEngine;

public class TargetMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _runSpeed = 4f;
    [SerializeField] private float _arrivalThreshold = 0.25f;

    private Transform _target;
    private Rotator _rotator;
    private float _arrivalThresholdSqr;
    private float _currentSpeed;    
    
    public event Action<Vector2> DirectionChanged;
    
    public event Action DestinationReached;
    
    public bool IsDestinationReached { get; private set; }
    
    private bool HasTarget => _target != null;

    private bool IsCanMove => HasTarget && !IsDestinationReached;


    private void Awake()
    {
        IsDestinationReached = true;
        _arrivalThresholdSqr = _arrivalThreshold * _arrivalThreshold;
        _currentSpeed = _moveSpeed;
    }

    public void Move()
    {
        if (IsCanMove)
        {
            MoveImplementation();
        }
    }

    public void EnableRun()
    {
        _currentSpeed =  _runSpeed;
    }

    public void DisableRun()
    {
        _currentSpeed = _moveSpeed;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
        IsDestinationReached = target == null;
        
        if (IsDestinationReached)
            DestinationReached?.Invoke();
    }
    
    public void Stop()
    {
       SetTarget(null);
    }

    private void MoveImplementation()
    {
        Vector3 targetPosition = _target.position;
        targetPosition.y = transform.position.y;

        float step = _currentSpeed * Time.deltaTime;
        Vector3 newPos = Vector3.MoveTowards(transform.position, targetPosition, step);

        var distance = (transform.position - _target.position).sqrMagnitude;

        if (distance < _arrivalThresholdSqr)
        {
            SetTarget(null);
            return;
        }
        
        Vector3 direction = (newPos - transform.position).normalized;
        
        if (direction != Vector3.zero)
        {
            DirectionChanged?.Invoke(direction);
        }
        
        transform.position = newPos;
    }
}