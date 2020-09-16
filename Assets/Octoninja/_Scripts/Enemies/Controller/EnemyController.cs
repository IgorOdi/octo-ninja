using Octoninja.Combat.Interface;
using Octoninja.Combat.Model;
using Octoninja.Enemies.Controller.Common;
using Octoninja.StateMachine;
using UnityEngine;

namespace Octoninja.Enemies.Controller {

    public class EnemyController : StateController, IDamageable {

        public int Health { get; protected set; }
        public Rigidbody2D rb { get; private set; }
        private EnemyVisualController visualController;

        void Start () => Initialize ();

        public override void Initialize () {

            rb = GetComponent<Rigidbody2D> ();
            visualController = GetComponentInChildren<EnemyVisualController> ();
            base.Initialize ();
        }

        public virtual void CauseDamage (Damager damager) {

            Health -= damager.Damage;

            visualController.Shake (damager.StaggerDuration);
            int side = damager.WorldPoint.x < transform.position.x ? 1 : -1;
            rb.AddForce (damager.ImpactForce * rb.mass * side);

            var hitState = GetStateFromType<CommonHitState> ();
            hitState.StateDuration = damager.StaggerDuration;
            SetState (GetStateFromType<CommonHitState> (), cancel : true);
        }
    }
}