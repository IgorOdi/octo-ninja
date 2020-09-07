using UnityEngine;

namespace Octoninja.Combat.Controller {

    public abstract class ProjectileController : MonoBehaviour {

        public Projectile Projectile;
        public float CurrentSpeed { get; protected set; }
        public bool IsMoving => rb.velocity.magnitude != 0;

        private bool shouldBeMoving;
        private Rigidbody2D rb;

        public virtual void Initialize () {

            CurrentSpeed = Projectile.StartingSpeed;
            shouldBeMoving = true;
            rb = GetComponent<Rigidbody2D> ();
        }

        protected virtual void Update () {

            if (shouldBeMoving)
                rb.velocity = transform.right * CurrentSpeed;
        }

        public void Stop () {

            shouldBeMoving = false;
            rb.velocity = Vector2.zero;
        }

        protected virtual void OnTriggerEnter2D (Collider2D other) { }
    }
}