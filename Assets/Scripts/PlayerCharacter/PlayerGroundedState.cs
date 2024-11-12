using Skill;
using UnityEngine;

namespace PlayerCharacter
{
    public class PlayerGroundedState : PlayerState
    {
        //GroundState用于保证只有在Idle和Move这两个地面状态下才能调用某些函数
        public PlayerGroundedState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
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

            if (Input.GetKeyDown(KeyCode.R))
                stateMachine.ChangeState(player.Blackhole);

            if (Input.GetKeyDown(KeyCode.Mouse1) && HasNoSword())
                stateMachine.ChangeState(player.aimSword);
            
            if (Input.GetKeyDown(KeyCode.Q))
                stateMachine.ChangeState(player.PlayerCounterAttack);
            
            if (Input.GetKeyDown(KeyCode.Mouse0))
                stateMachine.ChangeState(player.PrimaryAttackState);
            
            if (!player.IsGroundDetected())
                 stateMachine.ChangeState(player.AirState);
            
            if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
                stateMachine.ChangeState(player.JumpState); 
        }

        private bool HasNoSword()
        {
            if (!player.sword)
            {
                return true;
            }
            
            player.sword.GetComponent<Sword_Skill_Controller>().ReturnSword();
            return false;
        }
    }
}