using UnityEngine;

namespace Octoninja.Combat.Model {

    public enum AttackType {

        MELEE,
        RANGED,
    }

    [CreateAssetMenu (menuName = "Player/Attack", fileName = "New Player Attack")]
    public class Attack : ScriptableObject {

        public string AttackName;

        public AttackType AttackType;
        public GameObject projectile;

        public Damager Damager;

        [Header ("Timing")]
        public float Delay;
        public float Duration;
        public float RecoveryTime;

        [Header ("Side Effects")]
        //public Vector2 PushForce;

        public string AnimationName;
    }
}