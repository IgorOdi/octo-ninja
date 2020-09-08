using UnityEngine;

namespace Octoninja.Utils {

    public static class InstantiateUtils {

        public static T Instantiate<T> () where T : Component {

            GameObject g = new GameObject (typeof (T).Name);
            var component = g.AddComponent<T> ();
            return component;
        }
    }
}