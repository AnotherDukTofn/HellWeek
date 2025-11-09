using System;
using UnityEngine;

public class UIManager : MonoBehaviour {
    [SerializeField] private GameManager gameManager;

    private void OnEnable() {
        gameManager.OnWinGame += OnWinEvent;
        gameManager.OnLoseGame += OnLoseEvent;
    }

    private void OnDisable() {
        gameManager.OnWinGame -= OnWinEvent;
        gameManager.OnLoseGame -= OnLoseEvent;
    }

    void OnWinEvent() {
        PauseGame();
    }
    
    void OnLoseEvent() {
        PauseGame();
    }

    void PauseGame() {
        Time.timeScale = 0f;
        Debug.Log("Game paused");
    }

    void ResumeGame() {
        Time.timeScale = 1f;
    }
}
