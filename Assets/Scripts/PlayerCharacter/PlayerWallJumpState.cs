using UnityEngine;

namespace PlayerCharacter
{
    public class PlayerWallJumpState : PlayerState
    {
        public PlayerWallJumpState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
        {
        }
        public override void Enter()
        {
            base.Enter();
            stateTimer = .4f;
            player.SetVelocity(5 * -player.facingDir , player.jumpForce);
        }
        public override void Exit()
        {
            base.Exit();
        }
        public override void Update()
        {
            base.Update();
            if(stateTimer < 0)
                stateMachine.ChangeState(player.AirState);

            if (player.IsGroundDetected())
                stateMachine.ChangeState(player.IdleState);
        }
    }
}