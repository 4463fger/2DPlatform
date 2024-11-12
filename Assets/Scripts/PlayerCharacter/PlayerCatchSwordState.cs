using UnityEngine;

namespace PlayerCharacter
{
    public class PlayerCatchSwordState: PlayerState
    {
        private Transform sword;
        public PlayerCatchSwordState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            sword = player.sword.transform;
            
            if (player.transform.position.x > sword.position.x && player.facingDir == 1)
                player.Filp();
            else if (player.transform.position.x < sword.position.x && player.facingDir == -1)
                player.Filp();

            rb.velocity = new Vector2(player.swordReturnImpact * -player.facingDir, rb.velocity.y);
        }
        
        public override void Exit()
        {
            base.Exit();
            player.StartCoroutine("BusyFor", .1f);
        }
        
        public override void Update()
        {
            base.Update();

            if (triggerCalled)
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }
    }
}