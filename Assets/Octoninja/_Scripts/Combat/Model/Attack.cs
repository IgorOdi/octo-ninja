using UnityEngine;

namespace Octoninja.Combat.Model {

    [CreateAssetMenu (menuName = "Player/Attack", fileName = "New Player Attack")]
    public class Attack : ScriptableObject {

        [Header ("Damage")]
        public Damager Damager;

        [Header ("Timing")]
        public float Delay;
        public float Duration;
        public float RecoveryTime;

        [Header ("Side Effects")]
        public Vector2 PushForce;
        public bool ShakeScreen;
        public float ScreenShakeIntensity;

        public string AnimationName;
    }
}