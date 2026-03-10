using System;
using System.Collections;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private float _groundRadius = 0.2f;
    [SerializeField] private LayerMask _groundLayer;
    
    [Range(0f, 1f)]
    [SerializeField] private float _checkInterval = 0.1f;

    private PhysicsMover _physicsMover;
    private Coroutine _groundCheckCoroutine;
    private WaitForSeconds _wait;

    public bool IsGrounded { get; private set; }

    private void Awake()
    {
        _wait = new WaitForSeconds(_checkInterval);
    }

    private void OnEnable()
    {
        _groundCheckCoroutine = StartCoroutine(GroundCheckLoop());
    }

    private void OnDisable()
    {
        if (_groundCheckCoroutine != null)
            StopCoroutine(_groundCheckCoroutine);
    }

    private IEnumerator GroundCheckLoop()
    {
        while (true)
        {
            IsGrounded = Physics2D.OverlapCircle(transform.position, _groundRadius, _groundLayer);
            
            yield return _wait;
        }
    }
}