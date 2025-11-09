using System.Collections;
using UnityEngine;

public class Cell : MonoBehaviour {
    [SerializeField] private bool isOccupied;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color hitColor;
    
    public bool IsOccupied() => isOccupied;

    public void SetOccupied(bool value) {
        isOccupied = value;
    }

    public void Hit() {
        StopAllCoroutines();
        StartCoroutine(HitEffect());
    }
    
    private IEnumerator HitEffect() {
        spriteRenderer.color = hitColor;
        yield return new WaitForSeconds(0.25f);
        spriteRenderer.color = normalColor;
    }
}
