using UnityEngine;
using System;

public class GameManager : MonoBehaviour {


    [SerializeField] private Unit playerUnit;
    [SerializeField] private Unit enemyUnit;

    private void OnEnable() {
        playerUnit.OnDie += EndGame;
        enemyUnit.OnDie += EndGame;
    }

    private void OnDisable() {
        playerUnit.OnDie -= EndGame;
        enemyUnit.OnDie -= EndGame;
    }

    //Check Win/Lose
    public void EndGame() { }
    //Invoke Win/Lose event to UI Manager

}
