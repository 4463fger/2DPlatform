using UnityEngine;

namespace EnemyCharacter.Skeleton
{
    public class Enemy_Skeleton : Enemy
    {
        #region States
        public SkeletonIdleState idleState { get; private set; }
        public SkeletonMoveState moveState { get; private set; }
        public SkeletonBattleState battleState { get; private set; }
        public SkeletonAttackState attackState { get; private set; }
        public SkeletonStunnedState StunnedState { get; private set; }

        #endregion
        protected override void Awake()
        {
            base.Awake();

            idleState = new SkeletonIdleState(this, StateMachine, "Idle", this);
            moveState = new SkeletonMoveState(this, StateMachine, "Move", this);
            battleState = new SkeletonBattleState(this, StateMachine, "Move", this);
            attackState = new SkeletonAttackState(this, StateMachine, "Attack", this);
            StunnedState = new SkeletonStunnedState(this, StateMachine, "Stunned", this);
        }

        protected override void Start()
        {
            base.Start();
            StateMachine.Init(idleState);
        }

        protected override void Update()
        {
            base.Update();
        }

        public override bool CanBeStunned()
        {
            if (base.CanBeStunned())
            {
                StateMachine.ChangeState(StunnedState);
                return true;
            }
            return false;
        }
    }
}