using Octoninja.StateMachine;

namespace Octoninja.Enemies.Controller.Common {

    public class CommonHitState : State {

        public override bool TimeState => true;

        public override void OnStateEnter () {

            base.OnStateEnter ();
        }
    }
}