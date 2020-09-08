using System;
using Octoninja.Combat.Interface;
using Octoninja.Combat.Model;
using Octoninja.Global;
using Octoninja.Utils;
using UnityEngine;

namespace Octoninja.Combat.Controller {

    public class DamagerController : MonoBehaviour {

        public bool IsColliderActive { get { return collisionArea.enabled; } }
        protected Damager damager;
        protected Action<bool> onHit;

        private BoxCollider2D collisionArea;
        private void Awake () => collisionArea = GetComponentInChildren<BoxCollider2D> ();

        public virtual void ActivateCollider (Damager damager, float duration, Action<bool> hit) {

            ActivateCollider (damager, hit);
            this.RunDelayed (duration, DisableCollider);
        }

        public virtual void ActivateCollider (Damager damager, Action<bool> hit) {

            onHit = hit;
            this.damager = damager;
            this.damager.WorldPoint = transform.position;

            collisionArea.size = damager.Size;
            collisionArea.offset = damager.Offset;
            collisionArea.enabled = true;

            GetComponentInChildren<SpriteRenderer> ().enabled = true;
        }

        public virtual void DisableCollider () {

            collisionArea.enabled = false;
            GetComponentInChildren<SpriteRenderer> ().enabled = false;
        }

        public virtual void OnDamageCollision (Collider2D other) {

            if (!other.tag.Equals ("Damageable")) return;

            var damageable = other.GetComponentInChildren<IDamageable> ();
            damageable.CauseDamage (damager);

            if (damager.ShakeScreen)
                SingletonManager.GetSingleton<Cameras.CameraManager> ().ShakeCurrentCamera (damager.ScreenShakeDuration,
                    damager.ScreenShakeIntensity, true);

            onHit?.Invoke (true);
        }

        private void OnTriggerEnter2D (Collider2D other) => OnDamageCollision (other);
    }
}