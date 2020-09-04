using Octoninja.Combat.Interface;
using Octoninja.Combat.Model;
using UnityEngine;

namespace Octoninja.Enemies.Controller {

    public class DummyController : MonoBehaviour, IDamageable {

        public void CauseDamage (Damager damager) {

            GetComponentInChildren<EnemyVisualController> ().Shake ();
            int side = damager.WorldPoint.x > transform.position.x ? -1 : 1;
            GetComponent<Rigidbody2D> ().AddForce (Vector2.right * side * damager.ImpactForce);
        }
    }
}