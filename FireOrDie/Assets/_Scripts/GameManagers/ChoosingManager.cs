using UnityEngine;

public class ChoosingManager : MonoBehaviour {
    public ActionType actionType { get; private set; }
    [SerializeField] private int target;
    [SerializeField] private Unit allyUnit;

    public enum ActionType {
        None, Fire, Dodge, Reload
    }
    
    public IAction ResolveAction() {
        if (actionType == ActionType.Fire) {
            return new FireAction(target, allyUnit);
        }

        if (actionType == ActionType.Dodge) {
            return new DodgeAction(target, allyUnit);
        }

        if (actionType == ActionType.Reload) {
            return new ReloadAction(allyUnit);
        }
        
        return new FireAction(1, allyUnit);
    }

    public void SetTarget(int target) {
        this.target = target;
        Debug.Log($"chosen target: {this.target}");
    }

    public void SetActionType(ActionType actionType) {
        this.actionType = actionType;
        Debug.Log($"chosen action type: {this.actionType}");
    }

    public void ResetAction() {
        actionType = ActionType.None;
    }
}