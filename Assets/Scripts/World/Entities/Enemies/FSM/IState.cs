using System;
using System.Collections.Generic;

namespace FSM {

    public interface IState {

        event Action     OnNeedsReplan;

        event StateEvent OnEnter;
        event StateEvent OnExit;

        FiniteStateMachine FSM        { get; }
        string             Name       { get; }
        bool               HasStarted { get; set; }
        
        Dictionary<StateMachineInputs, IState>     Transitions        { get; set; }

        IState Configure(FiniteStateMachine fsm);

        void                       Enter(IState from, Dictionary<StateMachineInputs, object> transitionParameters);
        void                       UpdateLoop();
        Dictionary<StateMachineInputs, object> Exit(IState to);

        IState ProcessInput();

    }

    public delegate void StateEvent(IState from, IState to);
}