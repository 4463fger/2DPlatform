using UnityEngine;

namespace PlayerCharacter
{
    //角色状态类
    public class PlayerState
    {
        protected PlayerStateMachine stateMachine;
        protected Player player;
        protected float stateTimer;

        protected bool triggerCalled; 
        protected Rigidbody2D rb;
        
        protected float xInput;
        protected float yInput;
        private string animBoolName;


        public PlayerState(Player _player,PlayerStateMachine _playerStateMachine,string _animBoolName)
        {
            this.player = _player;
            this.stateMachine = _playerStateMachine;
            this.animBoolName = _animBoolName;
        }
        
        public virtual void Enter()
        {
            player.anim.SetBool(animBoolName,true);
            rb = player.rb;
            triggerCalled = false;
        }

        public virtual void Update()
        {
            stateTimer -= Time.deltaTime;
            
            xInput = Input.GetAxisRaw("Horizontal");
            yInput = Input.GetAxisRaw("Vertical");
            player.anim.SetFloat("yVelocity" , rb.velocity.y);
        }

        public virtual void Exit()
        {
            player.anim.SetBool(animBoolName,false);
        }

        //判断动画是否完成
        public virtual void AnimationFinishedTrigger()
        {
            triggerCalled = true;
        }
    }
}
