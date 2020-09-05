﻿using Octoninja.Input;
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

        private static T Instantiate<T> () where T : Component {

            GameObject g = new GameObject (typeof (T).Name);
            var component = g.AddComponent<T> ();
            DontDestroyOnLoad (g);
            return component;
        }
    }
}