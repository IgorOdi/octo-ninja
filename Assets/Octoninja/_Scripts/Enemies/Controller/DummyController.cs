using Octoninja.Combat.Interface;
using Octoninja.Combat.Model;
using UnityEngine;

namespace Octoninja.Enemies.Controller {

    public class DummyController : MonoBehaviour, IDamageable {

        public void CauseDamage (Damager damager) {

            GetComponentInChildren<EnemyVisualController> ().Shake ();
            int side = damager.WorldPoint.x > transform.position.x ? -1 : 1;
            var rb = GetComponent<Rigidbody2D> ();
            rb.AddForce (Vector2.one * side * rb.mass * damager.ImpactForce * 1.25f);
        }
    }
}