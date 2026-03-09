using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _groundRadius = 0.2f;
    [SerializeField] private LayerMask _groundLayer;

    private PhysicsMover _physicsMover;
    
    public bool IsGrounded { get; private set; }

    private void FixedUpdate()
    {
        IsGrounded = Physics2D.OverlapCircle(transform.position, _groundRadius, _groundLayer);
    }
}