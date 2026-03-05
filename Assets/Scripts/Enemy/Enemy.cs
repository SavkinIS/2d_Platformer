using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Mover _mover;
    [SerializeField] private EnemyAnimator _animator;
    [SerializeField] private float _waitingTime = 2;

    private StateMachine _stateMachine;
    public float WaitngTime => _waitingTime;

    private void Start()
    {
        _stateMachine.ChangeState(typeof(IdleState));
    }

    private void Update()
    {
        _stateMachine.Tick();
    }

    public void PlayIdle()
    {
        _animator.PlayIdle();
    }

    public void PlayMoveAnimation()
    {
        _animator.PlayMove(true);
    }

    public void Initialize(Path path)
    {
        _stateMachine = new StateMachine(this,  _mover, path);
    }
}