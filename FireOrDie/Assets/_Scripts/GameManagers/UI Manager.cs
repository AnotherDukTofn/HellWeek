using System;
using UnityEngine;
using System.Collections;
using TMPro;

public class UIManager : MonoBehaviour {
    [SerializeField] private GameManager gameManager;
    [SerializeField] private RectTransform pauseMenu;
    [SerializeField] private RectTransform backgroundPanel;
    [SerializeField] private RectTransform startPanel;
    [SerializeField] private PanelMover mover;
    [SerializeField] private TMP_Text countingText;

    private void OnEnable() {
        gameManager.OnWinGame += PauseTime;
        gameManager.OnLoseGame += PauseTime;
    }

    private void OnDisable() {
        gameManager.OnWinGame -= PauseTime;
        gameManager.OnLoseGame -= PauseTime;
    }

    private void Start() {
        StartCoroutine(StartGame());
    }

    public void PauseGame() {
        PauseTime();
        mover.Move(backgroundPanel, new Vector2(0, backgroundPanel.anchoredPosition.y));
        mover.Move(pauseMenu, new Vector2(pauseMenu.anchoredPosition.x, 0));
    }

    public void ResumeGame() {
        ResumeTime();
        mover.Move(backgroundPanel, new Vector2(mover.hideX, backgroundPanel.anchoredPosition.y));
        mover.Move(pauseMenu, new Vector2(pauseMenu.anchoredPosition.x, mover.hideY));
    }

    private void PauseTime() {
        Time.timeScale = 0f;
    }

    private void ResumeTime() {
        Time.timeScale = 1f;
    }

    private IEnumerator StartGame() {
        PauseTime();
        mover.Move(startPanel, new Vector2(0, 0));
        yield return new WaitForSecondsRealtime(0.4f);
        for (int i = 3; i >= 0; i--) {
            countingText.text =  i.ToString();
            yield return new WaitForSecondsRealtime(1f);
        }
        startPanel.gameObject.SetActive(false);
        ResumeTime();
    }
}