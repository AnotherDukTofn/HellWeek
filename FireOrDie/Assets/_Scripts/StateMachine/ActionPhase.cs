public class ActionPhase : IState {
    private TurnManager _turnManager;

    public ActionPhase(TurnManager turnManager) {
        this._turnManager = turnManager;
    }

    public void Enter() {
        _turnManager.ActionExecute();
    }

    public void Execute() { }

    public void Exit() {
        _turnManager.CheckTurnLimit();
    }
}