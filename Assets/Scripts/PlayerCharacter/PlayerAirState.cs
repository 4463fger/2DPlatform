using UnityEngine;

namespace PlayerCharacter
{
    public class PlayerAirState : PlayerState
    {
        public PlayerAirState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }
        
        public override void Update()
        {
            base.Update();

            if (player.IsWallDetected())    
                stateMachine.ChangeState(player.wallSlide);
            
            if (player.IsGroundDetected())
                stateMachine.ChangeState(player.IdleState);

            if (xInput != 0)
                player.SetVelocity(player.moveSpeed * .8f * xInput,rb.velocity.y);
        }

    }
}