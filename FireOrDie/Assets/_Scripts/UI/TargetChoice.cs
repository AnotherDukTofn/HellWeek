using UnityEngine;

public class TargetChoice : MonoBehaviour {
    [field: SerializeField] public int Choice { get; private set; }
    [SerializeField] private ChoosingManager choosingManager;

    public void OnClick() {
        choosingManager.SetTarget(Choice);
    }
}
