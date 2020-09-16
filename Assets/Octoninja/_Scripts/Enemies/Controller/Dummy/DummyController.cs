using Octoninja.StateMachine;

namespace Octoninja.Enemies.Controller.Dummy {

    public class DummyController : EnemyController {

        protected override State GetNextState () {

            if (LastStateWas<DummyHitState> ()) {

                return GetStateFromType<DummyIdleState> ();
            } else {

                return null;
            }
        }
    }
}