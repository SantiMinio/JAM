using UnityEngine;

namespace FSM
{
    public class FSMBrain : MonoBehaviour
    {
        FiniteStateMachine fsm;
        [SerializeField] FSMMakeTransitions[] states = new FSMMakeTransitions[0];


        public void Initialize()
        {
            fsm = new FiniteStateMachine(states[0].state, StartCoroutine);

            for (int i = 0; i < states.Length; i++)
            {
                states[i].SetStateTransitions(fsm);
            }
        }

        public void SendInput(StateMachineInputs input)
        {
            fsm.SendInput(input);
        }

        public bool CanTransitionTo(StateMachineInputs input) => fsm.CanTransitionTo(input);

        public string CurrentState => fsm.CurrentState.Name;

        public void ActivateBrain()
        {
            fsm.Active = true;
        }

        public void DesactivateBrain()
        {
            fsm.Active = false;
        }

    }
}