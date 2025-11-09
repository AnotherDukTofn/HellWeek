using UnityEngine;
using System;

public class GameManager : MonoBehaviour {
    [Header("References")]
    [SerializeField] private TurnManager turnManager;
    [SerializeField] private DamageTracker damageTracker;
    
    [SerializeField] private Unit playerUnit;
    [SerializeField] private Unit enemyUnit;
    
    public event Action OnWinGame;
    public event Action OnLoseGame;

    private void OnEnable() {
        playerUnit.OnDie += IsWinGame;
        enemyUnit.OnDie += IsWinGame;
        turnManager.OnTurnLimit += IsWinGame;
    }

    private void OnDisable() {
        playerUnit.OnDie -= IsWinGame;
        enemyUnit.OnDie -= IsWinGame;
        turnManager.OnTurnLimit -= IsWinGame;
    }

    public void IsWinGame() {
        if (playerUnit.IsDead && enemyUnit.IsDead) {
            if (playerUnit.CurrentHealth > enemyUnit.CurrentHealth) {
                OnWinGame?.Invoke();
                Debug.Log("Win Game!");
            }
            else {
                OnLoseGame?.Invoke();
                Debug.Log("Lose Game!");
            }
        }
        else if (playerUnit.IsDead && !enemyUnit.IsDead) {
            OnLoseGame?.Invoke();
            Debug.Log("Lose Game!");
        }
        else if (!playerUnit.IsDead && enemyUnit.IsDead) {
            OnWinGame?.Invoke();
            Debug.Log("Win Game!");
        }
        else {
            if (damageTracker.PlayerDamageDealt >= damageTracker.PlayerDamageTaken) {
                OnWinGame?.Invoke();
                Debug.Log("Win Game!");
            }
            else if (playerUnit.CurrentHealth / playerUnit.MaxHealth >= enemyUnit.CurrentHealth / enemyUnit.MaxHealth) {
                OnWinGame?.Invoke();
                Debug.Log("Win Game!");
            }
            else {
                OnLoseGame?.Invoke();
                Debug.Log("Lose Game!");
            }
        }
    }
}
