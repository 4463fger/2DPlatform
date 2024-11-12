using Skill;
using UnityEngine;

namespace PlayerCharacter
{
    public class PlayerDashState : PlayerState
    {
        public PlayerDashState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.skill.clone.CreateClone(player.transform,Vector3.zero);
            stateTimer = player.dashDuration;
        }
        public override void Exit()
        {
            base.Exit();
            player.SetVelocity(0,rb.velocity.y);
        }
        
        public override void Update()
        {
            base.Update();

            if (!player.IsGroundDetected() && player.IsWallDetected())
                stateMachine.ChangeState(player.wallSlide);
            
            //这个写在Update里，防止在x轴方向减速了，同时Y轴写成0，以防止dash还没有完成就从空中掉下去了
            player.SetVelocity(player.dashSpeed * player.facingDir,0);
            
            if(stateTimer < 0)//当timer < 0 切换
                stateMachine.ChangeState(player.IdleState);
        }

    }
}