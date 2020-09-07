using System.Collections;
using System.Collections.Generic;
using Octoninja.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Octoninja.Global {

    public class SceneManager : MonoBehaviour {

        public SceneController CurrentSceneController { get; private set; }
        public Scene CurrentScene { get; private set; }

        public void Initialize () {

            this.SubscribeAsSingleton ();
            CurrentSceneController = GetSceneController ();
            CurrentSceneController.OnSceneLoad ();
        }

        public void LoadScene (string sceneName) {

            SingletonManager.GetSingleton<InputManager> ().ClearKeys ();
            
            string previousScene = GetCurrentActiveScene ().name;
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync (sceneName, LoadSceneMode.Additive).completed += (op) => {

                var previousSceneController = CurrentSceneController;
                CurrentScene = UnityEngine.SceneManagement.SceneManager.GetSceneByName (sceneName);

                UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync (previousScene).completed += (op2) => {

                    previousSceneController.OnSceneUnload ();

                    UnityEngine.SceneManagement.SceneManager.SetActiveScene (CurrentScene);
                    CurrentSceneController = GetSceneController ();
                    CurrentSceneController.OnSceneLoad ();
                };
            };
        }

        private SceneController GetSceneController () {

            Scene currentScene = GetCurrentActiveScene ();
            var roots = currentScene.GetRootGameObjects ();
            foreach (GameObject g in roots) {

                if (g.TryGetComponent<SceneController> (out SceneController sceneController)) {

                    return sceneController;
                }
            }

            throw new System.Exception ("No Scene Controller found on current active scene");
        }

        private Scene GetCurrentActiveScene () => UnityEngine.SceneManagement.SceneManager.GetActiveScene ();
    }
}