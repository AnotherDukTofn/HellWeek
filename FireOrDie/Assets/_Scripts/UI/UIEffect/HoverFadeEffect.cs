using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverFadeEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField] private Color normalColor;
    [SerializeField] private Color hoverColor;
    [SerializeField] private Image image;
    
    public void OnPointerEnter(PointerEventData eventData) {
        image.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData) {
        image.color = normalColor;
    }
}