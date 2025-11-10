using UnityEngine;
using UnityEngine.UI;

public class TargetChoice : MonoBehaviour {
    [field: SerializeField] public int Choice { get; private set; }
    [SerializeField] private ChoosingManager choosingManager;
    [SerializeField] private Image image;
    [SerializeField] private Color selectedColor;
    [SerializeField] private Color originalColor;

    void Awake() {
        image = GetComponent<Image>();
    }

    public void OnClick() {
        choosingManager.SetTarget(Choice);
        image.color = selectedColor;
    }

    public void ResetColor() {
        image.color = originalColor;
    }
}
