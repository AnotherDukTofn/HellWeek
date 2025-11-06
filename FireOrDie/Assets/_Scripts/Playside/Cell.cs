using UnityEngine;

public class Cell : MonoBehaviour {
    [SerializeField] private bool isOccupied;
    
    public bool IsOccupied() => isOccupied;

    public void SetOccupied(bool value) {
        isOccupied = value;
    }
}
