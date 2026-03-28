using System;
using UnityEngine;

public abstract class PlayerAnimatorBase : MonoBehaviour
{
    public event Action AttackEnded;
    public event Action AttackDealDamage;
    public event Action HurtAnimationEnded;
    
    protected void CallAttackEnd()
    {
        AttackEnded?.Invoke();
    }

    protected void CallDealDamage()
    {
        AttackDealDamage?.Invoke();
    }

    protected void CallHurtEnded()
    {
        HurtAnimationEnded?.Invoke();
    }

    public abstract void Run(bool state);
    public abstract void Jump(bool state);
    public abstract void Fall(bool state);
    public abstract void Attack();
    public abstract void Dead();
    public abstract void Hurt();
}

public class PlayerAnimator : PlayerAnimatorBase
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rigidbody;

    private readonly int RunHash = Animator.StringToHash("IsRun");
    private readonly int IsGroundedHash = Animator.StringToHash("IsFall");
    private readonly int VerticalSpeedHash = Animator.StringToHash("IsJump");
    private readonly int AttackHash = Animator.StringToHash("IsAttack");
    private readonly int DeadHash = Animator.StringToHash("IsDead");
    private readonly int HurtHash = Animator.StringToHash("IsHurt");

    public override void Run(bool state)
    {
        _animator.SetBool(RunHash, state);
        _animator.SetBool(IsGroundedHash, false);
        _animator.SetBool(VerticalSpeedHash, false);
        _animator.SetBool(HurtHash, false);
        _animator.SetBool(DeadHash, false);
        _animator.SetBool(AttackHash, false);
        _animator.SetBool(IsGroundedHash, false);
    }

    public override void Jump(bool state)
    {
        _animator.SetBool(VerticalSpeedHash, state);
        _animator.SetBool(RunHash, false);
        _animator.SetBool(IsGroundedHash, false);
        _animator.SetBool(IsGroundedHash, false);
        _animator.SetBool(HurtHash, false);
        _animator.SetBool(DeadHash, false);
        _animator.SetBool(AttackHash, false);
        _animator.SetBool(IsGroundedHash, false);
    }

    public override void Fall(bool state)
    {
        _animator.SetBool(IsGroundedHash, state);
        _animator.SetBool(RunHash, false);
        _animator.SetBool(VerticalSpeedHash, false);
    }

    public override void Attack()
    {
        _animator.SetBool(AttackHash, true);
        _animator.SetBool(VerticalSpeedHash, false);
        _animator.SetBool(RunHash, false);
        _animator.SetBool(IsGroundedHash, false);
    }

    public void AttackStarted()
    {
        _animator.SetBool(AttackHash, false);
    }

    public void AttackEnd()
    {
        CallAttackEnd();
    }

    public void DealDamage()
    {
        CallDealDamage();
    }

    public void HurtEnded()
    {
        CallHurtEnded();
    }

    public override void Dead()
    {
        _animator.SetBool(DeadHash, true);
        _animator.SetBool(HurtHash, false);
        _animator.SetBool(AttackHash, false);
        _animator.SetBool(VerticalSpeedHash, false);
        _animator.SetBool(RunHash, false);
        _animator.SetBool(IsGroundedHash, false);
    }

    public override void Hurt()
    {
        _animator.SetBool(HurtHash, true);
        _animator.SetBool(DeadHash, false);
        _animator.SetBool(AttackHash, false);
        _animator.SetBool(VerticalSpeedHash, false);
        _animator.SetBool(RunHash, false);
        _animator.SetBool(IsGroundedHash, false);
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