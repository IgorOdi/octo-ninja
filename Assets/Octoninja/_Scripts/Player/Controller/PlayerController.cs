using DG.Tweening;
using Octoninja.Global;
using Octoninja.Input;
using Octoninja.Player.Model;
using Octoninja.Utils;
using UnityEngine;

namespace Octoninja.Player.Controller {

    [RequireComponent (typeof (Rigidbody2D))]
    public class PlayerController : MonoBehaviour {

        public PlayerData PlayerData = new PlayerData ();
        public PlayerAttackController AttackController { get; private set; }
        public TentacleController TentacleController { get; private set; }
        public bool CanMove { get; private set; } = true;

        private Rigidbody2D rb;
        private Animator animator;

        #region Input Reading

        private float InputValue { get { return inputManager.GetAxis (KeyAxis.HORIZONTAL); } }
        private InputManager inputManager;
        private InputKey tentacleKey;
        private InputKey jumpKey;

        #endregion

        #region Level Data Capture

        private int lookingSide = 1;
        private bool grounded;
        private RaycastHit2D hit;
        private LayerMask groundMask;
        private const float GROUND_RAY_DISTANCE = .1f;
        private const string GROUND_LAYER = "Ground";

        #endregion

        public void Awake () => Initialize ();
        public void Initialize () {

            rb = GetComponent<Rigidbody2D> ();
            animator = GetComponentInChildren<Animator> ();
            AttackController = GetComponentInChildren<PlayerAttackController> ();
            TentacleController = GetComponentInChildren<TentacleController> ();
            groundMask = LayerMask.GetMask (GROUND_LAYER);

            inputManager = SingletonManager.GetSingleton<InputManager> ();
            jumpKey = inputManager.GetKey (OctoKey.JUMP);
            tentacleKey = inputManager.GetKey (OctoKey.TENTACLE);
            jumpKey.OnKeyDown += Jump;
            tentacleKey.OnKeyDown += () => TentacleController.ThrowTentacle ();

            AttackController.Initialize (this, inputManager);
            TentacleController.Initialize (this, inputManager);
        }

        public void Update () {

            rb.gravityScale = AttackController.IsRecovering ? 0 : 1;
            //if (!CanMove) return;
            if (AttackController.IsRecovering) {

                rb.velocity = Vector2.zero;
                return;
            }

            hit = Physics2D.Raycast (transform.position, Vector2.down, GROUND_RAY_DISTANCE, groundMask);
            grounded = hit.collider != null;

            if (InputValue > 0) lookingSide = 1;
            else if (InputValue < 0) lookingSide = -1;

            LookTo (lookingSide);
            rb.velocity = Vector2.right * InputValue * PlayerData.GetCurrentSpeed (lookingSide, InputValue) + Vector2.up * rb.velocity.y;
        }

        public void PushPlayer (Vector2 force, float duration) {

            CanMove = false;
            rb.velocity = Vector2.zero;
            rb.AddForce (Vector2.right * lookingSide * force);
            this.RunDelayed (duration, () => CanMove = true);
        }

        public void PullToPosition (Vector2 position, float pullSpeed) {

            CanMove = false;
            float distance = Vector2.Distance (transform.position, position);
            float duration = distance / pullSpeed;
            transform.DOMove (position - Vector2.up * 0.75f, duration)
                .SetEase (Ease.OutCubic)
                .OnComplete (() => CanMove = true);
        }

        public void LookTo (int side) {

            transform.localScale = new Vector3 (side, 1, 1);
        }

        private void Jump () {

            if (!grounded || !CanMove || AttackController.IsRecovering) return;

            rb.velocity = new Vector2 (rb.velocity.x, 0);
            rb.AddForce (Vector2.up * PlayerData.JumpForce);
        }
    }
}