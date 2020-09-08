using Octoninja.Combat.Controller;
using Octoninja.Input;

namespace Octoninja.Player.Controller {

    public class PlayerAttackController : AttackController {

        private PlayerController controller;
        private InputManager inputManager;
        private InputKey slashKey;

        private float comboTime;
        private const float MAX_COMBO_TIME = 2f;

        public void Initialize (PlayerController controller, InputManager inputManager) {

            this.controller = controller;
            this.inputManager = inputManager;

            slashKey = inputManager.GetKey (OctoKey.SLASH);
            slashKey.OnKeyDown += () => NextComboAttack (0);
            base.Initialize ();
        }

        protected virtual void Update () {

            comboTime += UnityEngine.Time.deltaTime;
            if (comboTime > MAX_COMBO_TIME) {

                comboTime = 0;
                currentAtkIndex = 0;
            }
        }

        protected override bool NextComboAttack (int comboIndex) {

            if (!base.NextComboAttack (comboIndex)) return false;
            comboTime = 0;

            var atk = GetCurrentAttackInfo (comboIndex);
            //controller.PushPlayer (atk.PushForce, atk.Duration);
            return true;
        }
    }
}