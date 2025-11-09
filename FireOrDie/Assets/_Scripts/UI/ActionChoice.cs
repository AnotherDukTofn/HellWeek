using System;
using UnityEngine;

public class ActionChoice : MonoBehaviour {
    public ChoosingManager.ActionType choice;
    [SerializeField] private ChoosingManager playerChoosingManager;
    [SerializeField] private ChoosingUI choosingUI;
    
    public void OnClick() {
        playerChoosingManager.SetActionType(choice);
        choosingUI.NavigateToTargetPanel();
    }
}
