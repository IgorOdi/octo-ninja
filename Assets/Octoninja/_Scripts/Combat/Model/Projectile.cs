using System;

namespace Octoninja.Combat.Controller {

    [Serializable]
    public class Projectile {

        public float StartingSpeed;
        public bool DestroyAlongTime;
        public float DestroyTime;
    }
}