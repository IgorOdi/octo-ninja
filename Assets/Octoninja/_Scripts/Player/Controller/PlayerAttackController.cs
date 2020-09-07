using Octoninja.Combat.Controller;
using Octoninja.Input;

namespace Octoninja.Player.Controller {
    
    public class PlayerAttackController : AttackController {

        private PlayerController controller;
        private InputManager inputManager;
        private InputKey slashKey;

        public void Initialize (PlayerController controller, InputManager inputManager) {

            this.controller = controller;
            this.inputManager = inputManager;

            slashKey = inputManager.GetKey (OctoKey.SLASH);
            slashKey.OnKeyDown += () => NextComboAttack (0);
            base.Initialize ();
        }

        protected override bool NextComboAttack (int comboIndex) {

            if (!base.NextComboAttack (comboIndex)) return false;

            var atk = GetCurrentAttackInfo (comboIndex);
            //controller.PushPlayer (atk.PushForce, atk.Duration);
            return true;
        }
    }
}