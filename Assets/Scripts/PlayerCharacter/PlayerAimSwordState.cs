using UnityEngine;

namespace PlayerCharacter
{
    public class PlayerAimSwordState : PlayerState
    {
        public PlayerAimSwordState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            player.skill.sword.DotsActive(true);
        }

        public override void Exit()
        {
            base.Exit();

            player.StartCoroutine("BusyFor", .2f);
        }
        
        public override void Update()
        {
            base.Update();
            
           player.SetZeroVelocity();
            
            if (Input.GetKeyUp(KeyCode.Mouse1))
                stateMachine.ChangeState(player.IdleState);

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (player.transform.position.x > mousePosition.x && player.facingDir == 1)
                player.Filp();
            else if (player.transform.position.x < mousePosition.x && player.facingDir == -1)
                player.Filp();
        }
    }
}