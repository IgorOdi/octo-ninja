using System;
using UnityEngine;

namespace Octoninja.Combat.Model {

    [Serializable]
    public class Damager {

        [Header ("Damage")]
        public int Damage;
        public Vector2 Size;
        public Vector2 Offset;
        public Vector2 ImpactForce;
        public float StaggerDuration;

        [Header ("Screen Shake")]
        public bool ShakeScreen;
        public float ScreenShakeDuration = 0.15f;
        public float ScreenShakeIntensity = 0.15f;
        public float ScreenShakeFrequency = 4f;

        public Vector2 WorldPoint { get; set; }
    }
}