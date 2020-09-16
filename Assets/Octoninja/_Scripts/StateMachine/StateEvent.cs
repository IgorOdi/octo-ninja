using System;

namespace Octoninja.StateMachine {

    public class StateEvent {

        public readonly float Time;
        public readonly Action OnEventHappen;
        public bool Happenend { get; set; }

        public StateEvent (float time, Action onEventHappen) {

            Time = time;
            OnEventHappen = onEventHappen;
        }

        public void CallEvent () {

            if (Happenend) throw new Exception ("Event already happened");

            OnEventHappen?.Invoke ();
            Happenend = true;
        }
        
        public void ResetEvent () => Happenend = false;
    }
}