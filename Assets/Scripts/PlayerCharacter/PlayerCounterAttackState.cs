using EnemyCharacter;
using UnityEngine;

namespace PlayerCharacter
{
    public class PlayerCounterAttackState: PlayerState
    {
        public PlayerCounterAttackState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            stateTimer = player.counterAttackDuration;
            player.anim.SetBool("SuccessfulCounterAttack",false);
        }
        
        public override void Exit()
        {
            base.Exit();
        }
        
        public override void Update()
        {
            base.Update();
            
            player.SetZeroVelocity();

            Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

            foreach (var hit in colliders)
            {
                if (hit.GetComponent<Enemy>() is not null)
                {
                    if (hit.GetComponent<Enemy>().CanBeStunned())
                    {
                        stateTimer = 10;
                        player.anim.SetBool("SuccessfulCounterAttack",true);
                    }
                }
            }

            if (stateTimer < 0 || triggerCalled)
                stateMachine.ChangeState(player.IdleState);
        }
    }
}