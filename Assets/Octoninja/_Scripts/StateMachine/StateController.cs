using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Octoninja.StateMachine {

    public class StateController : MonoBehaviour {

        public State CurrentState { get; protected set; }
        public List<State> States { get; private set; } = new List<State> ();
        protected State StartingState => States[0];

        public virtual void Initialize () {

            States = GetComponentsInChildren<State> ().ToList ();
            States.ForEach ((s) => s.OnStateSetup (this));
            SetState (StartingState);
        }

        public void ChangeToNextState (bool cancel = false) {

            State state = GetNextState ();
            SetState (state, cancel);
        }

        protected virtual void SetState (State state, bool cancel = false) {

            if (cancel) CurrentState.OnStateCancel ();

            CurrentState = state;
            CurrentState.OnStateEnter ();
        }

        protected State GetStateFromType<T> () where T : State {

            for (int i = 0; i < States.Count; i++) {

                if (States[i].GetType ().Equals (typeof (T)) || States[i].GetType ().IsSubclassOf (typeof (T)))
                    return States[i];
            }

            throw new System.Exception ("No states with the type found");
        }

        protected virtual State GetNextState () { return null; }

        protected bool LastStateWas<T> () where T : State {

            return CurrentState.GetType ().Equals (typeof (T));
        }

        protected virtual void Update () {

            if (CurrentState != null)
                CurrentState.OnStateUpdate ();
        }
    }
}