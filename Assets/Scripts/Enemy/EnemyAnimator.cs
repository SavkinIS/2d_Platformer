using System;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private readonly int MoveHash = Animator.StringToHash("Move");
    private readonly int AttackHash = Animator.StringToHash("IsAttack");
    private readonly int DeadHash = Animator.StringToHash("IsDead");
    private readonly int HurtHash = Animator.StringToHash("IsHurt");

    public event Action AttackAnimationEnded;
    public event Action AttackDealDamage;
    public event Action HurtAnimationEnded;
    public event Action DeadAnimationEnded;
    
    public void PlayMove(bool state)
    {
        _animator.SetBool(MoveHash,state);
    }

    public void PlayIdle()
    {
        _animator.SetBool(MoveHash, false);
        
        _animator.SetBool(DeadHash, false);
        _animator.SetBool(HurtHash, false);
        _animator.SetBool(AttackHash, false);
    }

    public void PlayDeath()
    {
        _animator.SetBool(DeadHash, true);
        
        _animator.SetBool(MoveHash, false);
        _animator.SetBool(HurtHash, false);
        _animator.SetBool(AttackHash, false);
    }

    public void DeadExecute()
    {
        DeadAnimationEnded?.Invoke();
    }
    
    public void PlayAttack()
    {
        _animator.SetBool(AttackHash, true);
        
        _animator.SetBool(MoveHash, false);
        _animator.SetBool(HurtHash, false);
        _animator.SetBool(DeadHash, false);
    }
    
    public void AttackEnd()
    {
        AttackAnimationEnded?.Invoke();
    }

    public void AttackStarted()
    {
        _animator.SetBool(AttackHash, false);
    }

    public void DealDamage()
    {
        AttackDealDamage?.Invoke();
    }
    
    public void PlayHurt()
    {
        _animator.SetBool(HurtHash, true);
        _animator.SetBool(MoveHash, false);
        _animator.SetBool(AttackHash, false);
        _animator.SetBool(DeadHash, false);
    }

    public void HurtEnded()
    {
        _animator.SetBool(HurtHash, false);
        HurtAnimationEnded?.Invoke();
    }
}