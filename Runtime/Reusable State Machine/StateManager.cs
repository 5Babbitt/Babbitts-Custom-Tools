using System;
using System.Collections.Generic;
using UnityEngine;

namespace FiveBabbittGames
{
    public abstract class StateMachine<EState> : MonoBehaviour where EState : Enum
    {
        protected Dictionary<EState, BaseState<EState>> States = new();
        protected BaseState<EState> CurrentState;

        protected bool isTransitioningState = false;

        private void Start()
        {
            CurrentState.EnterState();
        }

        private void Update()
        {
            EState nextStateKey = CurrentState.GetNextState();

            
            if (isTransitioningState) 
                return;
            
            if (nextStateKey.Equals(CurrentState.Statekey))
                CurrentState.UpdateState();
            else
                TransitionToState(nextStateKey);
        }

        public void TransitionToState(EState stateKey)
        {
            isTransitioningState = true;
            CurrentState.ExitState();
            CurrentState = States[stateKey];
            CurrentState.EnterState();
            isTransitioningState = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            CurrentState.OnTriggerEnter(other);
        }

        private void OnTriggerExit(Collider other)
        {
            CurrentState.OnTriggerExit(other);
        }

        private void OnTriggerStay(Collider other)
        {
            CurrentState.OnTriggerStay(other);
        }
    }
}
