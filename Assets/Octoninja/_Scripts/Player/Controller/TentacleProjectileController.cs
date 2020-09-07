using Octoninja.Combat.Controller;
using Octoninja.Player.Controller;
using UnityEngine;

namespace Octoninja.Player {

    public class TentacleProjectileController : ProjectileController {

        private PlayerController playerController;
        private TentacleController tentacleController;
        private LineRenderer lineRenderer;

        private Vector3[] points = new Vector3[2];

        void Awake () {

            lineRenderer = GetComponentInChildren<LineRenderer> ();
        }

        public void Initialize (PlayerController _playerController, TentacleController _tentacleController) {

            base.Initialize ();
            playerController = _playerController;
            tentacleController = _tentacleController;
            lineRenderer.useWorldSpace = true;
        }

        protected override void Update () {

            base.Update ();
            points[0] = tentacleController.transform.position;
            points[1] = transform.position;
            lineRenderer.SetPositions (points);
        }

        protected override void OnTriggerEnter2D (Collider2D other) {

            if (other.CompareTag ("Holdable") || other.CompareTag ("Damageable")) {

                Stop ();
                playerController.PullToPosition (transform.position, tentacleController.PullSpeed);
                Destroy (gameObject);
            }
        }
    }
}