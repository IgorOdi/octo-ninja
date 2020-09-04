using DG.Tweening;
using UnityEngine;

namespace Octoninja.Enemies.Controller {

    public class EnemyVisualController : MonoBehaviour {

        private SpriteRenderer spriteRenderer;
        private Vector3 startPosition;

        void Awake () => Initialize ();
        public void Initialize () {

            spriteRenderer = GetComponent<SpriteRenderer> ();
            startPosition = transform.localPosition;
        }

        public void Shake (float duration = 0.5f) {

            transform.DOShakePosition (duration, 0.25f, 30)
                .OnComplete (ResetVisualPosition);
        }

        private void ResetVisualPosition () {

            transform.localPosition = startPosition;
        }
    }
}