using System.Collections.Generic;
using UnityEngine;

namespace Octoninja.StateMachine {

    public enum ExitMode {

        EXIT,
        CANCEL
    }

    public class State : MonoBehaviour {

        public virtual float StateDuration { get; set; }
        public virtual bool TimeState { get; }
        protected float Timer { get; private set; }

        protected StateController stateController;
        protected List<StateEvent> EventList { get; private set; } = new List<StateEvent> ();

        public virtual void OnStateSetup (StateController stateController) {

            this.stateController = stateController;
        }

        public virtual void OnStateEnter () {

            Timer = 0;
            EventList.ForEach ((x) => x.ResetEvent ());
        }

        public virtual void OnStateUpdate () {

            Timer += Time.deltaTime;
            for (int i = 0; i < EventList.Count; i++) {

                if (EventList[i].Time <= Timer && !EventList[i].Happenend)
                    EventList[i].CallEvent ();
            }

            if (TimeState && Timer >= StateDuration) OnStateExit ();
        }

        public virtual void OnStateExit () { stateController.ChangeToNextState (); }
        public virtual void OnStateCancel () { }
    }
}