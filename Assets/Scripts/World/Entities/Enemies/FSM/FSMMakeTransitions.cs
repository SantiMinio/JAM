using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    [System.Serializable]
    public class FSMMakeTransitions
    {
        public MonoBaseState state;

        [SerializeField] InputAndState[] transitions = new InputAndState[0];

        public void SetStateTransitions(FiniteStateMachine stateMachine)
        {
            for (int i = 0; i < transitions.Length; i++)
            {
                stateMachine.AddTransition(transitions[i].input, state, transitions[i].state);
            }
        }
    }
}