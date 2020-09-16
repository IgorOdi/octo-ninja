using Cinemachine;
using DG.Tweening;
using UnityEngine;

namespace Octoninja.Cameras {

    public class CameraController : MonoBehaviour {

        public CinemachineVirtualCamera VirtualCamera { get; protected set; }
        public CinemachineImpulseSource ImpulseSource { get; protected set; }
        public bool IsShaking { get; protected set; }

        void Awake () {

            VirtualCamera = GetComponentInChildren<CinemachineVirtualCamera> ();
            ImpulseSource = GetComponentInChildren<CinemachineImpulseSource> ();
        }

        public void Shake (float duration, float intensity, float frequency, bool overrideCurrentShake = true) {

            if (IsShaking && !overrideCurrentShake) {

                Debug.LogWarning ("Camera is already shaking");
                return;
            }

            IsShaking = true;
            ImpulseSource.m_ImpulseDefinition.m_AmplitudeGain = intensity;
            ImpulseSource.m_ImpulseDefinition.m_FrequencyGain = frequency;
            ImpulseSource.m_ImpulseDefinition.m_TimeEnvelope.m_AttackTime = duration / 2;
            ImpulseSource.m_ImpulseDefinition.m_TimeEnvelope.m_DecayTime = duration / 2;
            ImpulseSource.GenerateImpulse ();
        }
    }
}