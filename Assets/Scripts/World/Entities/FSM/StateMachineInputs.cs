using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public enum StateMachineInputs
    {
        ToIdle,
        ToMove,
        ToTakeDamage,
        ToDeath,
        ToDash,
        ToJump,
        ToAttack,
        ToSecondAttack
    }
}