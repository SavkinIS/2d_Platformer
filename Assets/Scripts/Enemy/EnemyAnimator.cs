using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private readonly int MoveHash = Animator.StringToHash("Move");

    public void PlayMove(bool state)
    {
        _animator.SetBool(MoveHash,state);
    }

    public void PlayIdle()
    {
        _animator.SetBool(MoveHash, false);
    }
}