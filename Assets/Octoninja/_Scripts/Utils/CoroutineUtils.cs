using System;
using System.Collections;
using UnityEngine;

namespace Octoninja.Utils
{

    public static class CoroutineUtils {

        public static void RunDelayed (this MonoBehaviour mono, float time, Action callback) {

            mono.StartCoroutine (WaitFor (time, callback));
        }

        private static IEnumerator WaitFor (float time, Action callback) {

            yield return new WaitForSeconds (time);
            callback?.Invoke ();
        }
    }
}