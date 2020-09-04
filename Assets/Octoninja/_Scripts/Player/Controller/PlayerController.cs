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

        public Rigidbody2D Rigidbody { get; private set; }
        public Animator Animator { get; private set; }

        public bool CanMove { get; set; } = true;

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

            Rigidbody = GetComponent<Rigidbody2D> ();
            Animator = GetComponentInChildren<Animator> ();
            AttackController = GetComponentInChildren<PlayerAttackController> ();
            groundMask = LayerMask.GetMask (GROUND_LAYER);

            inputManager = SingletonManager.GetSingleton<InputManager> ();
            jumpKey = inputManager.GetKey (OctoKey.JUMP);
            jumpKey.OnKeyDown += Jump;

            AttackController.Initialize (this, inputManager);
        }

        public void Update () {

            if (!CanMove) return;

            hit = Physics2D.Raycast (transform.position, Vector2.down, GROUND_RAY_DISTANCE, groundMask);
            grounded = hit.collider != null;

            if (!AttackController.IsAttacking) {

                lookingSide = inputManager.GetMousePosition ().x > transform.position.x ? 1 : -1;
                LookTo (lookingSide);
                Rigidbody.velocity = Vector2.right * InputValue * PlayerData.GetCurrentSpeed (lookingSide, InputValue) + Vector2.up * Rigidbody.velocity.y;
            }
        }

        public void PushPlayer (Vector2 force, float duration) {

            CanMove = false;
            Rigidbody.velocity = Vector2.zero;
            Rigidbody.AddForce (Vector2.right * lookingSide * force);
            this.RunDelayed (duration, () => CanMove = true);
        }

        public void LookTo (int side) {

            transform.localScale = new Vector3 (side, 1, 1);
        }

        private void Jump () {

            if (!grounded) return;

            Rigidbody.velocity = new Vector2 (Rigidbody.velocity.x, 0);
            Rigidbody.AddForce (Vector2.up * PlayerData.JumpForce);
        }
    }
}