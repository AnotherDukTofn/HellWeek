using System;

public interface IAction {
    public void Execute(Action onComplete = null);    
}

public class FireAction : IAction {
    private int _target;
    private Unit _self;

    public FireAction(int target, Unit self) {
        this._target = target;
        this._self = self;
    }

    public void Execute(Action onComplete = null) {
        _self.FireTo(_target, onComplete);
    }
}

public class DodgeAction : IAction {
    private int _target;
    private Unit _self;

    public DodgeAction(int target, Unit self) {
        this._target = target;
        this._self = self;
    }

    public void Execute(Action onComplete = null) {
        // Pass callback xuống Unit để đợi movement xong
        this._self.DodgeTo(_target, onComplete);
    }
}

public class ReloadAction : IAction {
    private Unit _self;

    public ReloadAction(Unit self) {
        this._self = self;
    }

    public void Execute(Action onComplete = null) {
        _self.Reload(onComplete);
    }
}