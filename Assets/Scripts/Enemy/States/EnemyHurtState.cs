namespace EnemySystem
{
    public class EnemyHurtState : IEnemyState
    {
        private readonly EnemyStateMachine _enemyStateMachine;
        private readonly EnemyAnimator _enemyAnimator;
        private readonly Health _health;
        private readonly Enemy _enemy;

        public EnemyHurtState(EnemyAnimator enemyAnimator, EnemyStateMachine enemyStateMachine, Health health, Enemy enemy)
        {
            _enemyAnimator = enemyAnimator;
            _enemyStateMachine = enemyStateMachine;
            _health  = health;
            _enemy = enemy;
        }

        public void Enter()
        {
            _enemyAnimator.PlayHurt();
            _enemyAnimator.HurtAnimationEnded += HurtAnimationEnded;
        }

        public void Exit()
        {
            _enemyAnimator.HurtAnimationEnded -= HurtAnimationEnded;
        }

        private void HurtAnimationEnded()
        {
            if (_health.Current > 0)
            {
                if (_enemy.IsTargetInAttackRange())
                {
                    _enemyStateMachine.ChangeState(typeof(EnemyAttackState));
                }
                else
                {
                    _enemyStateMachine.ChangeState(typeof(EnemyIdleState));
                }
            }
        }
    }
}