using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PanelMover : MonoBehaviour {
    [SerializeField] private float moveDuration = 0.25f;
    public float hideX, hideY;
    
    private readonly Dictionary<RectTransform, Coroutine> runningCoroutines = new();

    public void Move(RectTransform panel, Vector2 target) {
        if (runningCoroutines.TryGetValue(panel, out Coroutine active))
            StopCoroutine(active);

        runningCoroutines[panel] = StartCoroutine(DoMove(panel, target, moveDuration));
    }

    private IEnumerator DoMove(RectTransform panel, Vector2 target, float duration) {
        float elapsed = 0f;
        Vector2 startPosition = panel.anchoredPosition;

        while (elapsed < duration) {
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            t = 1f - Mathf.Pow(1f - t, 3); // ease-out-cubic

            panel.anchoredPosition = Vector2.Lerp(startPosition, target, t);
            yield return null;
        }

        panel.anchoredPosition = target;
        runningCoroutines.Remove(panel);
    }
}