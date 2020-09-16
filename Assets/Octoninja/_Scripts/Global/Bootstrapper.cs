using Octoninja.Input;
using Octoninja.Pooling;
using UnityEngine;

namespace Octoninja.Global {

    public class Bootstrapper : MonoBehaviour {

        [RuntimeInitializeOnLoadMethod (RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnStartGame () {

            var inputManager = Instantiate<InputManager> ();
            inputManager.Initialize ();

            inputManager.RegisterKey (OctoKey.SLASH, KeyCode.Mouse0);
            inputManager.RegisterKey (OctoKey.TENTACLE, KeyCode.Mouse1);
            inputManager.RegisterKey (OctoKey.JUMP, KeyCode.W);
            inputManager.RegisterKey (OctoKey.PAUSE, KeyCode.Escape);
        }

#if UNITY_EDITOR
        [RuntimeInitializeOnLoadMethod (RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void OnStartFirstScene () {

            var sceneManager = Instantiate<SceneManager> ();
            sceneManager.Initialize ();

            var poolManager = Instantiate<PoolManager> ();
            poolManager.Initialize ();
        }
#endif

        private static T Instantiate<T> () where T : Component {

            var component = Utils.InstantiateUtils.Instantiate<T> ();
            DontDestroyOnLoad (component);
            return component;
        }
    }
}