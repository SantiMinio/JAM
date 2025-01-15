using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    [System.Serializable]
    public struct InputAndState
    {
        public StateMachineInputs input;
        public MonoBaseState state;
    }
}