﻿using UnityEngine;

namespace PlayerCharacter
{
    public class PlayerJumpState : PlayerState
    {
        public PlayerJumpState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);
        }
        public override void Exit()
        {
            base.Exit();
        }
        
        public override void Update()
        {
            base.Update();
            
            if (rb.velocity.y < 0)
            {
                stateMachine.ChangeState(player.AirState);
            }
        }

    }
}