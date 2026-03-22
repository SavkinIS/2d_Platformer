using System;
using System.Collections.Generic;

namespace PlayerSystem
{
    public class PlayerStateMachine
    {
        private readonly Dictionary<Type, IPlayerState> _states;


        public PlayerStateMachine(PlayerAnimator playerAnimator, PhysicsMover physicsMover,
            PlayerInputBuffer inputBuffer,
            Attacker attacker)
        {
            _states = new Dictionary<Type, IPlayerState>()
            {
                { typeof(PlayerIdleState), new PlayerIdleState(playerAnimator) },
                { typeof(PlayerMovementState), new PlayerMovementState(playerAnimator, inputBuffer) },
                { typeof(PlayerAttackState), new PlayerAttackState(this, playerAnimator, attacker) },
                { typeof(PlayerJumpState), new PlayerJumpState(playerAnimator, physicsMover) },
                { typeof(PlayerFallState), new PlayerFallState(playerAnimator, physicsMover) },
                { typeof(PlayerHurtState), new PlayerHurtState(playerAnimator, this) },
                { typeof(DeathState), new DeathState(playerAnimator) },
            };
        }


        public IPlayerState CurrentState { get; private set; }
        public bool IsCurrentStateCanMove => CurrentState != null ? CurrentState.CanMove : false;
        public bool IsCurrentStateCanJump => CurrentState != null ? CurrentState.CanJump : false;
        public bool IsCurrentStateCanAttack => CurrentState != null ? CurrentState.CanAttack : false;

        public void Update()
        {
            CurrentState.Update();
        }

        public void ChangeState(Type type)
        {
            if (_states.TryGetValue(type, out IPlayerState state))
            {
                CurrentState?.Exit();
                CurrentState = state;
                CurrentState?.Enter();
            }
        }
    }
}