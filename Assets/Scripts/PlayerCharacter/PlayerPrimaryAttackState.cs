using UnityEngine;

namespace PlayerCharacter
{
    public class PlayerPrimaryAttackState : PlayerState
    {
        private int comboCounter;
        private float lastTimeAttacked;//距离上一次攻击的时间
        private float comboWindow = 2; //可以间隔的时间
        public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
        {
        }
        public override void Enter()
        {
            base.Enter();
            xInput = 0;

            if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
                comboCounter = 0;
            
            player.anim.SetInteger("ComboCounter",comboCounter);
            
            
            float attackDir = player.facingDir;
            if (xInput != 0)
                attackDir = xInput;
            
            player.SetVelocity(player.attackMovement[comboCounter].x * attackDir,player.attackMovement[comboCounter].y);

            stateTimer = .1f;
        }

        public override void Exit()
        {
            base.Exit();
            
            player.StartCoroutine("BusyFor" , .15f);

            comboCounter++;
            lastTimeAttacked = Time.time;
        }
        
        public override void Update()
        {
            base.Update();

            if (stateTimer < 0)
                player.SetZeroVelocity();
            
            if (triggerCalled)
                stateMachine.ChangeState(player.IdleState);
        }
    }
}