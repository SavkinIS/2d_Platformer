using System;
using UnityEngine;

public class TargetMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _arrivalThreshold = 0.25f;

    private Transform _target;
    private Rotator _rotator;
    private float _arrivalThresholdSqr;

    public event Action<Vector2> DirectionChanged;
    
    public event Action DestinationReached;
    
    public bool IsDestinationReached { get; private set; }
    
    private bool HasTarget => _target != null;

    private bool IsCanMove => HasTarget && !IsDestinationReached;


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
        
        if (IsDestinationReached)
            DestinationReached?.Invoke();
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