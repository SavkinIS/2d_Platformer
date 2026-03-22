namespace EnemySystem
{
    public class EnemyDeadState : IEnemyState
    {
        private Enemy _enemy;
        private readonly EnemyAnimator _enemyAnimator;

        public EnemyDeadState(Enemy enemy, EnemyAnimator enemyAnimator, EnemyStateMachine  stateMachine)
        {
            _enemy = enemy;
            _enemyAnimator = enemyAnimator;
        }

        public void Enter()
        {
            _enemyAnimator.DeadAnimationEnded += Execute;
            _enemyAnimator.PlayDeath();
        }

        public void Exit()
        {
            _enemyAnimator.DeadAnimationEnded -= Execute;
        }

        private void Execute()
        {
            _enemy.gameObject.SetActive(false);
            _enemyAnimator.enabled = false;
            _enemy.Dead();
        }
    }
}