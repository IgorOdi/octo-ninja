using UnityEngine;

namespace Octoninja.Global {

    public abstract class SceneController : MonoBehaviour {

        public virtual void OnSceneLoad () { }
        public virtual void OnSceneUnload () { }
    }
}