using UnityEngine;

namespace PlayerCharacter
{
    //FSM用来切换动画
    public class PlayerStateMachine
    {
        public PlayerState currentState { get; private set; }

        public void Init(PlayerState _startState)
        {
            currentState = _startState;
            currentState.Enter();
        }

        public void ChangeState(PlayerState _newState)
        {
            currentState.Exit();
            currentState = _newState;
            currentState.Enter();
        }
    }
}
