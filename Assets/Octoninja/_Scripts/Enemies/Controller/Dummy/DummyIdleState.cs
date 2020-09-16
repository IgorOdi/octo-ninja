using Octoninja.StateMachine;
using UnityEngine;

namespace Octoninja.Enemies.Controller.Dummy {

    public class DummyIdleState : State {

        protected DummyController StateController => (DummyController) stateController;

        public override void OnStateEnter () {

            base.OnStateEnter ();
            Debug.Log ("Entrou em estado de idle");
        }

        public override void OnStateUpdate () {

            base.OnStateUpdate ();
            Debug.Log ("Está em estado de idle");
        }

        public override void OnStateExit () {

            base.OnStateExit ();
            Debug.Log ("Saiu do estado de idle normalmente");
        }

        public override void OnStateCancel () {

            base.OnStateCancel ();
            Debug.Log ("Saiu do estado de idle por cancelamento");
        }
    }
}