using UnityEngine;

namespace Octoninja.Global.Scenes {

    public class PlaygroundSceneController2 : SceneController {

        public override void OnSceneLoad () {

            Debug.Log ("Carregou cena Playground2");
        }

        void Update() {

            if (UnityEngine.Input.GetKeyDown(KeyCode.Space)) {

                SingletonManager.GetSingleton<SceneManager>().LoadScene("Playground");
            }
        }
    }
}