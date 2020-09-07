using Octoninja.Input;
using Octoninja.Player;
using Octoninja.Player.Controller;
using UnityEngine;

public class TentacleController : MonoBehaviour {

    public float PullSpeed = 5;

    [SerializeField]
    private GameObject projectile;
    private InputManager inputManager;
    private PlayerController playerController;
    private Coroutine eulerCoroutine;

    public void Initialize (PlayerController _playerController, InputManager _inputManager) {

        inputManager = _inputManager;
        playerController = _playerController;
        inputManager = _inputManager;
    }

    public void ThrowTentacle () {

        Vector3 direction = inputManager.GetMouseDirection (transform.position);
        int adder = transform.localScale.x == 1 ? 0 : 180;
        float z = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
        var proj = Instantiate (projectile, transform.position, Quaternion.Euler (transform.forward * z));
        proj.GetComponent<TentacleProjectileController> ().Initialize (playerController, this);
    }
}