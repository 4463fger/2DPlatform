﻿namespace EnemyCharacter.Skeleton
{
    public class SkeletonMoveState : SkeletonGroundedState
    {
        public SkeletonMoveState(Enemy enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton enemy) : base(enemyBase, _stateMachine, _animBoolName, enemy)
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
            enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir , rb.velocity.y);

            if (enemy.IsWallDetected() || !enemy.IsGroundDetected())
            {
                enemy.Filp();
                stateMachine.ChangeState(enemy.idleState);
            }
        }

    }
}