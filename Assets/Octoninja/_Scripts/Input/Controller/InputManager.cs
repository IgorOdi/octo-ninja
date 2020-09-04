using System.Collections.Generic;
using Octoninja.Global;
using UnityEngine;

namespace Octoninja.Input {

    public class InputManager : MonoBehaviour {

        private Dictionary<OctoKey, InputKey> registeredKeys = new Dictionary<OctoKey, InputKey> ();

        private static readonly List<string> KEY_AXIS = new List<string> { "Horizontal", "Vertical" };
        private static readonly List<string> MOUSE_AXIS = new List<string> { "Mouse X", "Mouse Y", "Mouse ScrollWheel" };

        public void Initialize () {

            this.SubscribeAsSingleton ();
        }

        public InputKey RegisterKey (OctoKey keyID, KeyCode keyCode) {

            var newKey = new InputKey (keyCode);
            registeredKeys[keyID] = newKey;
            return newKey;
        }

        public void UnregisterKey (OctoKey keyID) {

            registeredKeys.Remove (keyID);
        }

        public InputKey GetKey (OctoKey keyID) {

            return registeredKeys[keyID];
        }

        public float GetAxis (KeyAxis axis, bool raw = true) {

            return raw ? UnityEngine.Input.GetAxisRaw (KEY_AXIS[(int) axis]) : UnityEngine.Input.GetAxis (KEY_AXIS[(int) axis]);
        }

        public float GetMouseAxis (MouseAxis axis) {

            return UnityEngine.Input.GetAxis (MOUSE_AXIS[(int) axis]);
        }

        public Vector2 GetMousePosition () {

            return Camera.main.ScreenToWorldPoint (UnityEngine.Input.mousePosition);
        }

        public void ClearKeys () {

            registeredKeys.Clear ();
        }

        public void Update () {

            foreach (KeyValuePair<OctoKey, InputKey> pair in registeredKeys) {

                if (UnityEngine.Input.GetKeyDown (pair.Value.KeyCode))
                    pair.Value.KeyDown ();

                if (UnityEngine.Input.GetKeyUp (pair.Value.KeyCode))
                    pair.Value.KeyUp ();

                if (UnityEngine.Input.GetKey (pair.Value.KeyCode))
                    pair.Value.KeyHold ();
            }
        }
    }
}