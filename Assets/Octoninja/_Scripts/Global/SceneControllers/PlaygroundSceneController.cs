using UnityEngine;

namespace Octoninja.Global.Scenes {

    public class PlaygroundSceneController : SceneController {

        public override void OnSceneLoad () {

            Debug.Log ("Carregou cena Playground");
        }

        void Update () {

            if (UnityEngine.Input.GetKeyDown (KeyCode.Space)) {

                SingletonManager.GetSingleton<SceneManager> ().LoadScene ("Playground2");
            }
        }
    }
}