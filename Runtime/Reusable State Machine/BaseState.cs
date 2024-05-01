using System;
using UnityEngine;

namespace FiveBabbittGames
{
    public abstract class BaseState<EState> where EState : Enum
    {
        public BaseState(EState key)
        {
            Statekey = key;
        }

        public EState Statekey { get; private set; }

        public abstract void EnterState();
        public abstract void ExitState();
        public abstract void UpdateState();
        public abstract EState GetNextState();
        public abstract void OnTriggerEnter(Collider collider);
        public abstract void OnTriggerExit(Collider collider);
        public abstract void OnTriggerStay(Collider collider);

    }
}
