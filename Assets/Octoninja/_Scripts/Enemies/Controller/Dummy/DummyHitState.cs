using Octoninja.Enemies.Controller.Common;
using UnityEngine;

namespace Octoninja.Enemies.Controller.Dummy {

    public class DummyHitState : CommonHitState {

        public override void OnStateEnter () {

            base.OnStateEnter ();
            Debug.Log ("Entrou em estado de hit");
        }

        public override void OnStateUpdate () {

            base.OnStateUpdate ();
            Debug.Log ("Está em estado de hit");
        }

        public override void OnStateExit () {

            Debug.Log ("Saiu do estado de hit normalmente");
            base.OnStateExit ();
        }

        public override void OnStateCancel () {

            base.OnStateCancel ();
            Debug.Log ("Saiu do estado de hit pro cancelamento");
        }
    }
}