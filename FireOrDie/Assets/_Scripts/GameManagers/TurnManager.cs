using System;
using UnityEngine;

public class TurnManager : MonoBehaviour {
    [Header("Turn Manager")]
    private StateMachine _stateMachine;
    [SerializeField] private string currentPhase;
    private int _currentTurn;
    private int _turnLimit;
    
    
    [Header("Choosing Settings")]
    [SerializeField] private ChoosingManager choosingManager;
    [SerializeField] private ChoosingUI choosingUI;
    [field: SerializeField] public float ChoosingDuration { get; private set; }
    public float choosingTimer;
    
    [Header("Unit Actions")]
    private IAction _playerAction;
    private IAction _enemyAction;
    public bool actionExecuted;

    public event Action onTurnLimit;

    public void ActionExecute() {
        _playerAction = choosingManager.ResolveAction();
        actionExecuted = false;

        _playerAction.Execute(() => {
            Debug.Log("Player action completed!");
            actionExecuted = true;
        });
        choosingManager.ResetAction();
    }

    public void CheckTurnLimit() {
        if (_currentTurn > _turnLimit) onTurnLimit?.Invoke();
    }
    
    public IAction GetPlayerAction() => _playerAction;
    public IAction GetEnemyAction() => _enemyAction;

    void Start() {
        Init();
        _currentTurn = 1;
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
        _stateMachine.AddTransition(action, choosing, () => actionExecuted);
        
        _stateMachine.SetState(choosing);
    }

    public void StartChoosing() {
        actionExecuted = false;
        choosingUI.NavigateToActionPanel();
    }

    public void CompleteChoosing() {
        choosingUI.TurnOffChoosing();
    }
}