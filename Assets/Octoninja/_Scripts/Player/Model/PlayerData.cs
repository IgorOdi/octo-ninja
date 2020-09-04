using System;

namespace Octoninja.Player.Model {

    [Serializable]
    public class PlayerData {

        public float Speed = 6f;
        public float SlowSpeed { get { return Speed / 2; } }
        public float JumpForce = 50f;

        public float GetCurrentSpeed (int lookingSide, float inputValue) {

            return lookingSide == inputValue ? Speed : SlowSpeed;
        }
    }
}