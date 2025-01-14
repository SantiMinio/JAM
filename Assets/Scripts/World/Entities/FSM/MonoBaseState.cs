using System;
using System.Collections.Generic;
using UnityEngine;

namespace FSM {

    public abstract class MonoBaseState: MonoBehaviour, IState {

        public virtual event Action     OnNeedsReplan;
        public event StateEvent OnEnter;
        public event StateEvent OnExit;
        
        public virtual string Name => name;

        public virtual bool HasStarted { get; set; }

        public FiniteStateMachine FSM => _fsm;

        public virtual Dictionary<StateMachineInputs, IState> Transitions { get; set; } = new Dictionary<StateMachineInputs, IState>();

        private FiniteStateMachine _fsm;


        public IState Configure(FiniteStateMachine fsm) {
            _fsm            =  fsm;
            _fsm.OnActive   += OnActive;
            _fsm.OnUnActive += OnUnActive;
            return this;
        }

        public virtual void Enter(IState from, Dictionary<StateMachineInputs, object> transitionParameters = null) {
            OnEnter?.Invoke(from, this);
            HasStarted = true;
        }

        public virtual Dictionary<StateMachineInputs, object> Exit(IState to) {
            OnExit?.Invoke(this, to);
            HasStarted = false;
            return null;
        }

        public abstract void UpdateLoop();

        protected virtual void OnActive() {}

        protected virtual void OnUnActive() {}

        public abstract IState ProcessInput();
    }
}