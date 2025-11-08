using UnityEngine;

public class ChoosingPhase : IState {
    private TurnManager _turnManager;

    public ChoosingPhase(TurnManager turnManager) {
        this._turnManager = turnManager;
    }

    public void Enter() {
        _turnManager.StartChoosing();
        _turnManager.choosingTimer = _turnManager.ChoosingDuration;
    }

    public void Execute() {
        _turnManager.choosingTimer -= Time.deltaTime;
        if (_turnManager.choosingTimer <= 0) _turnManager.choosingTimer = 0;
    }

    public void Exit() {
        _turnManager.CompleteChoosing();
        _turnManager.choosingTimer = _turnManager.ChoosingDuration;
    }
}
