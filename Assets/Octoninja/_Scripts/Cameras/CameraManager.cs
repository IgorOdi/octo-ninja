using Cinemachine;
using Octoninja.Global;
using UnityEngine;

namespace Octoninja.Cameras {

    public class CameraManager : MonoBehaviour {

        public CinemachineBrain CinemachineBrain { get; private set; }
        public CameraController PlayerCamera { get; private set; }
        public CameraController CurrentCamera { get; private set; }

        public void Initialize () {

            this.SubscribeAsSingleton ();
            PlayerCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraController> ();
            CinemachineBrain = PlayerCamera.GetComponentInChildren<CinemachineBrain> ();
            SetCurrentCamera (PlayerCamera);
        }

        public void SetCurrentCamera (CameraController cam, float blendTime = 2f) {

            if (CurrentCamera != null)
                CurrentCamera.gameObject.SetActive (false);
                
            cam.gameObject.SetActive (true);

            CurrentCamera = cam;
            CinemachineBrain.m_DefaultBlend.m_Time = blendTime;
        }

        public void ShakeCurrentCamera (float duration, float intensity, bool overrideCurrentShake = true) {

            CurrentCamera.Shake (duration, intensity, overrideCurrentShake);
        }
    }
}