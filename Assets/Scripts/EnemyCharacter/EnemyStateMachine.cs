﻿namespace EnemyCharacter
{
    public class EnemyStateMachine
    {
        public EnemyState currentState { get; private set; }

        public void Init(EnemyState _startState)
        {
            currentState = _startState;
            currentState.Enter();
        }

        public void ChangeState(EnemyState _newState)
        {
            currentState.Exit();
            currentState = _newState;
            currentState.Enter();
        }
    }
}