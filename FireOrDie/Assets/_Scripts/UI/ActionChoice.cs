using System;
using UnityEngine;

public class ActionChoice : MonoBehaviour {
    public ChoosingManager.ActionType choice;
    [SerializeField] private ChoosingManager choosingManager;
    [SerializeField] private ChoosingUI choosingUI;
    
    public void OnClick() {
        choosingManager.SetActionType(choice);
        choosingUI.NavigateToTargetPanel();
    }
}
