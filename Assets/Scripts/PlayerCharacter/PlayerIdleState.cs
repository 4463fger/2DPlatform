using UnityEngine;

namespace PlayerCharacter
{
    public class PlayerIdleState : PlayerGroundedState
    {
        public PlayerIdleState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
        {
            
        }

        public override void Enter()
        {
            base.Enter();
            
            //进入该状态移除速度
            player.SetZeroVelocity();
        }
        public override void Exit()
        {
            base.Exit();
        }
        public override void Update()
        {
            base.Update();
            if (xInput != 0 && !player.isBusy)
                stateMachine.ChangeState(player.MoveState);
        }
    }
}