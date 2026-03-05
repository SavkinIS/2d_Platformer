using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private float _arrivalThreshold = 0.25f;
    private float _arrivalThresholdSqr;
    private Transform _target;
    private Vector3 _direction;

    private bool CanMove => _target != null;
    public bool IsDestinationReached { get; private set; }

    private void Awake()
    {
        _arrivalThresholdSqr = _arrivalThreshold * _arrivalThreshold;
    }

    private void Update()
    {
        if (CanMove)
            Move();

    }

    public void SetTarget(Transform target)
    {
        _target = target;
        
        IsDestinationReached = target == null;
    }

    private void Move()
    {
        _direction = (_target.position - transform.position).normalized;
        Vector3 moveDirection = _direction * _moveSpeed * Time.deltaTime;
        
        _spriteRenderer.flipX = _direction.x > 0;
        
        transform.Translate(new Vector3(moveDirection.x, 0, moveDirection.z), Space.World);
        
        var distance = (transform.position - _target.position).sqrMagnitude;

        if (distance < _arrivalThresholdSqr)
        {
            SetTarget(null);
        }
    }
}