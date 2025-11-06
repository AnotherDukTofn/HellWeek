using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

namespace _Scripts.UI {
    public class HoverScaleEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        [SerializeField] private float hoverScale;
        [SerializeField] private float animationDuration = 0.1f;

        private void ScaleTo(Vector3 target) {
            StopAllCoroutines();
            StartCoroutine(ScaleAnimation(target));
        }

        private IEnumerator ScaleAnimation(Vector3 target) {
            float elapsed = 0f;
            Vector3 start = transform.localScale;

            while (elapsed < animationDuration) {
                elapsed += Time.unscaledDeltaTime;
                float t = Mathf.Clamp01(elapsed / animationDuration);
                transform.localScale = Vector3.Lerp(start, target, Mathf.SmoothStep(0, 1, t));
                yield return null;
            }

            transform.localScale = target;
        }

        public void OnPointerEnter(PointerEventData eventData) {
            ScaleTo(Vector3.one * hoverScale);
        }

        public void OnPointerExit(PointerEventData eventData) {
            ScaleTo(Vector3.one);
        }
    }
}