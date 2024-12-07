using System;
using System.Collections;
using Skill;
using UnityEngine;

namespace PlayerCharacter
{
    public class Player : Entity
    {
        [Header("攻击信息")] 
        public Vector2[] attackMovement;
        public float counterAttackDuration =.2f;
        
        public bool isBusy { get; private set; }
        [Header("移动信息")] 
        public float moveSpeed = 12f;
        public float jumpForce;
        public float swordReturnImpact;
        
        [Header("冲刺信息")]
        public float dashSpeed;
        public float dashDuration;
        public float dashDir { get; private set; }
        
        public SkillManager skill { get; private set;}
        public GameObject sword{ get; private set; }

        
        #region 状态
        public PlayerStateMachine stateMachine { get; private set; }
        public PlayerIdleState IdleState { get; private set; }
        public PlayerMoveState MoveState { get; private set; }
        public PlayerJumpState JumpState { get; private set; }
        public PlayerAirState AirState { get; private set; }
        public PlayerDashState DashState { get; private set; }
        public PlayerWallSliderState wallSlide { get; private set; }
        public PlayerWallJumpState wallJump { get; private set; }
        public PlayerPrimaryAttackState PrimaryAttackState { get; private set; }
        public PlayerCounterAttackState PlayerCounterAttack { get; private set; }
        public PlayerAimSwordState aimSword { get; private set; }
        public PlayerCatchSwordState CatchSword { get; private set; }
        public PlayerBlackholeState Blackhole { get; private set; }
        
        #endregion
        
        protected override void Awake()
        {
            base.Awake();
            stateMachine = new PlayerStateMachine();

            IdleState = new PlayerIdleState(this, stateMachine, "Idle");
            MoveState = new PlayerMoveState(this, stateMachine, "Move");
            JumpState = new PlayerJumpState(this, stateMachine, "Jump");
            AirState  = new PlayerAirState(this, stateMachine, "Jump");
            DashState = new PlayerDashState(this, stateMachine, "Dash");
            wallSlide = new PlayerWallSliderState(this, stateMachine, "WallSlide");
            wallJump = new PlayerWallJumpState(this, stateMachine, "Jump");
            
            PrimaryAttackState = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
            PlayerCounterAttack = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");

            aimSword = new PlayerAimSwordState(this, stateMachine, "AimSword");
            CatchSword = new PlayerCatchSwordState(this, stateMachine, "CatchSword");
            Blackhole = new PlayerBlackholeState(this, stateMachine, "Jump");
        }

        protected override void Start()
        {
            base.Start();
            
            skill = SkillManager.instance;
            //初始是idle状态
            stateMachine.Init(IdleState);
        }
        public IEnumerator BusyFor(float seconds)
        {
            isBusy = true;
            yield return new WaitForSeconds(seconds);
            isBusy = false;
        }
        protected override void Update()
        {
            base.Update();
            stateMachine.currentState.Update();
            CheckForDashInput();

            if (Input.GetKeyDown(KeyCode.F))
            {
                skill.crystal.CanUseSkill();
            }
        }

        public void AssignNewSword(GameObject newSword)
        {
            sword = newSword;
        }
        public void CatchTheSword()
        {
            stateMachine.ChangeState(CatchSword);
            Destroy(sword);
        }
        
        public void AnimationTrigger() => stateMachine.currentState.AnimationFinishedTrigger();

        private void CheckForDashInput()
        {
            if (IsWallDetected())
                return;

            if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.instance.dash.CanUseSkill())
            {
                dashDir = Input.GetAxisRaw("Horizontal");
                //默认冲刺方向
                if (dashDir == 0)
                    dashDir = facingDir;
                stateMachine.ChangeState(DashState);
            }
        }
    }
}
