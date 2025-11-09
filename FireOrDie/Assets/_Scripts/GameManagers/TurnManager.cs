using System;
using UnityEngine;

public class TurnManager : MonoBehaviour {
    [Header("Turn Manager")]
    private StateMachine _stateMachine;
    [SerializeField] private string currentPhase;
    [SerializeField] private int currentTurn;
    [SerializeField] private int turnLimit;
    
    
    [Header("Choosing Settings")]
    [SerializeField] private ChoosingManager playerChoosingManager;
    [SerializeField] private ChoosingManager enemyChoosingManager;
    [SerializeField] private ChoosingUI choosingUI;
    [field: SerializeField] public float ChoosingDuration { get; private set; }
    public float choosingTimer;
    
    [Header("Unit Actions")]
    private IAction _playerAction;
    private IAction _enemyAction;
    [field: SerializeField] public EnemyAI Enemy { get; private set; }
    public bool playerActionExecuted;
    public bool enemyActionExecuted;

    public event Action OnTurnLimit;

    public void ActionExecute() {
        _playerAction = playerChoosingManager.ResolveAction();
        _enemyAction = enemyChoosingManager.ResolveAction();

        playerActionExecuted = false;
        enemyActionExecuted  = false;

        bool playerDodges = _playerAction is DodgeAction;
        bool enemyDodges  = _enemyAction  is DodgeAction;

        if (playerDodges) {
            _playerAction.Execute(() => { playerActionExecuted = true; });
        }

        if (enemyDodges) {
            _enemyAction.Execute(() => { enemyActionExecuted = true; });
        }

        if (!playerDodges) {
            _playerAction.Execute(() => { playerActionExecuted = true; });
        }

        if (!enemyDodges) {
            _enemyAction.Execute(() => { enemyActionExecuted = true; });
        }

        playerChoosingManager.ResetAction();
        Debug.Log($"Enemy {enemyChoosingManager.actionType}");
    }


    public void CheckTurnLimit() {
        if (currentTurn >= turnLimit) {
            OnTurnLimit?.Invoke();
            Debug.Log("Turn limit Invoked");
        }
        else currentTurn++;
    }
    
    public IAction GetPlayerAction() => _playerAction;
    public IAction GetEnemyAction() => _enemyAction;

    void Start() {
        Init();
        currentTurn = 1;
    }

    void Update() {
        currentPhase = _stateMachine.GetCurrentState().GetType().Name;
        _stateMachine.Tick();
    }

    void Init() {
        _stateMachine = new StateMachine();
        
        ChoosingPhase choosing = new ChoosingPhase(this);
        ActionPhase action = new ActionPhase(this);
        
        _stateMachine.AddTransition(choosing, action, () => choosingTimer == 0);
        _stateMachine.AddTransition(action, choosing, () => playerActionExecuted && enemyActionExecuted);
        
        _stateMachine.SetState(choosing);
    }

    public void StartChoosing() {
        playerActionExecuted = false;
        choosingUI.NavigateToActionPanel();
    }

    public void CompleteChoosing() {
        choosingUI.TurnOffChoosing();
    }
}