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
        public bool CanMove { get; set; } = true;

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
            groundMask = LayerMask.GetMask (GROUND_LAYER);

            inputManager = SingletonManager.GetSingleton<InputManager> ();
            jumpKey = inputManager.GetKey (OctoKey.JUMP);
            jumpKey.OnKeyDown += Jump;

            AttackController.Initialize (this, inputManager);
        }

        public void Update () {

            CanMove = !AttackController.IsRecovering && !AttackController.IsAttacking;
            if (!CanMove) {

                rb.velocity = new Vector2 (0, rb.velocity.y);
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

        public void LookTo (int side) {

            transform.localScale = new Vector3 (side, 1, 1);
        }

        private void Jump () {

            if (!grounded) return;

            rb.velocity = new Vector2 (rb.velocity.x, 0);
            rb.AddForce (Vector2.up * PlayerData.JumpForce);
        }
    }
}