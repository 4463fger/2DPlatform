using UnityEngine;

namespace EnemyCharacter.Skeleton
{
    public class SkeletonAttackState : EnemyState
    {
        private Enemy_Skeleton enemy;
        public SkeletonAttackState(Enemy enemyBase, EnemyStateMachine _stateMachine, string _animBoolName,Enemy_Skeleton _enemy) : base(enemyBase, _stateMachine, _animBoolName)
        {
            this.enemy = _enemy;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();

            enemy.lastTimeAttacked = Time.deltaTime;
        }

        public override void Update()
        {
            base.Update();
            
            enemy.SetZeroVelocity();

            if (triggerCalled)
                stateMachine.ChangeState(enemy.battleState);
        }
    }
}