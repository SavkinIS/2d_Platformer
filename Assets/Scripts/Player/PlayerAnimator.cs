using System;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rigidbody;
    
    private readonly int RunHash = Animator.StringToHash("IsRun");
    private readonly int IsGroundedHash = Animator.StringToHash("IsFall");
    private readonly int VerticalSpeedHash = Animator.StringToHash("IsJump");
    private readonly int AttackHash = Animator.StringToHash("IsAttack");
    private readonly int DeadHash = Animator.StringToHash("IsDead");
    private readonly int HurtHash = Animator.StringToHash("IsHurt");

    public event Action AttackEnded;
    public event Action AttackDealDamage;
    public event Action HurtAnimationEnded;

    public void Run(bool state)
    {
        _animator.SetBool(RunHash,state);
        _animator.SetBool(IsGroundedHash, false);
        _animator.SetBool(VerticalSpeedHash, false);
        _animator.SetBool(HurtHash, false);
        _animator.SetBool(DeadHash,false);
        _animator.SetBool(AttackHash, false);
        _animator.SetBool(IsGroundedHash, false);
    }

    public void Idle()
    {
        _animator.SetBool(HurtHash, false);
        _animator.SetBool(DeadHash,false);
        _animator.SetBool(AttackHash, false);
        _animator.SetBool(VerticalSpeedHash, false);
        _animator.SetBool(RunHash, false);
        _animator.SetBool(IsGroundedHash, false);
    }

    public void Jump(bool state)
    {
        _animator.SetBool(VerticalSpeedHash, state);
        _animator.SetBool(RunHash, false);
        _animator.SetBool(IsGroundedHash, false);
        _animator.SetBool(IsGroundedHash, false);
        _animator.SetBool(HurtHash, false);
        _animator.SetBool(DeadHash,false);
        _animator.SetBool(AttackHash, false);
        _animator.SetBool(IsGroundedHash, false);
    }

    public void Fall(bool state)
    {
        _animator.SetBool(IsGroundedHash, state);
        _animator.SetBool(RunHash, false);
        _animator.SetBool(VerticalSpeedHash, false);
    }

    public void Attack()
    {
        _animator.SetBool(AttackHash, true);
        _animator.SetBool(VerticalSpeedHash, false);
        _animator.SetBool(RunHash, false);
        _animator.SetBool(IsGroundedHash, false);
    }

    public void AttackEnd()
    {
        AttackEnded?.Invoke();
    }

    public void AttackStarted()
    {
        _animator.SetBool(AttackHash, false);
    }

    public void DealDamage()
    {
        AttackDealDamage?.Invoke();
    }

    public void Dead()
    {
        _animator.SetBool(DeadHash, true);
        _animator.SetBool(HurtHash, false);
        _animator.SetBool(AttackHash, false);
        _animator.SetBool(VerticalSpeedHash, false);
        _animator.SetBool(RunHash, false);
        _animator.SetBool(IsGroundedHash, false);
    }
    
    public void Hurt()
    {
        _animator.SetBool(HurtHash, true);
        _animator.SetBool(DeadHash,false);
        _animator.SetBool(AttackHash, false);
        _animator.SetBool(VerticalSpeedHash, false);
        _animator.SetBool(RunHash, false);
        _animator.SetBool(IsGroundedHash, false);
    }

    public void HurtEnded()
    {
        HurtAnimationEnded?.Invoke();
    }
    
    public void HurtStarted()
    {
        _animator.SetBool(HurtHash, false);
    }
    
    public void StartDead()
    {
        _animator.SetBool(DeadHash, false);
    }
}