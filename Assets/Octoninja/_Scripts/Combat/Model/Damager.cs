using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Octoninja.Combat.Model {

    [Serializable]
    public class Damager {

        public int Damage;
        public Vector2 Size;
        public Vector2 Offset;
        public Vector2 ImpactForce;

        public bool ShakeScreen;
        public float ScreenShakeDuration;
        public float ScreenShakeIntensity;

        public Vector2 WorldPoint { get; set; }
    }
}