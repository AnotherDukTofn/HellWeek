using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Profiling;

public class ChoosingUI : MonoBehaviour {
    [Header("References")]
    [SerializeField] private ChoosingManager playerChoosingManager;
    [SerializeField] private RectTransform actionPanel;
    [SerializeField] private RectTransform targetPanel;
    [SerializeField] private GameObject[] actionChoices = new GameObject[3];
    [SerializeField] private GameObject[] targetChoices = new GameObject[3];
    
    [SerializeField] private Playside playerSide;
    
    [Header("Move Panel Settings")]
    [SerializeField] private float movePanelTime;
    [SerializeField] private float moveDownPos;
    [SerializeField] private float moveUpPos;
    
    //cached coroutine
    private Coroutine _actionPanelCoroutine;
    private Coroutine _targetPanelCoroutine;

    void Start() {
        for (int i = 0; i < actionChoices.Length; i++) {
            actionChoices[i] = actionPanel.GetChild(i).gameObject;
        }

        for (int i = 0; i < targetChoices.Length; i++) {
            targetChoices[i] = targetPanel.GetChild(i).gameObject;
        }
    }

    public void NavigateToTargetPanel() {
        ResetTargetPanel();
        MovePanel(actionPanel, moveDownPos, ref _actionPanelCoroutine);
        MovePanel(targetPanel, moveUpPos, ref _targetPanelCoroutine);
    }

    public void NavigateToActionPanel() {
        ResetActionPanel();
        MovePanel(targetPanel, moveDownPos, ref _targetPanelCoroutine);
        MovePanel(actionPanel, moveUpPos, ref _actionPanelCoroutine);
    }

    public void TurnOffChoosing() {
        MovePanel(actionPanel, moveDownPos, ref _actionPanelCoroutine);
        MovePanel(targetPanel, moveDownPos, ref _targetPanelCoroutine);
    }

    public void ResetTargetPanel() {
        targetPanel.gameObject.SetActive(true);
        foreach (GameObject child in targetChoices) {
            child.gameObject.GetComponent<TargetChoice>().ResetColor();
        }
        
        if (playerChoosingManager.actionType == ChoosingManager.ActionType.Dodge) {
            for (int i = 0; i < targetPanel.transform.childCount; i++) {
                bool occupied = playerSide.Cells[i].IsOccupied();
                targetChoices[i].SetActive(!occupied);
            }
        }
        else if (playerChoosingManager.actionType == ChoosingManager.ActionType.Reload) {
            targetPanel.gameObject.SetActive(false);
        }
        else {
            foreach (Transform child in targetPanel.transform) {
                child.gameObject.SetActive(true);
            }
        }
    }

    public void ResetActionPanel() {
        int currentAmmo = playerSide.GetAllyUnit().weapon.GetCurrentAmmo();
        int maxAmmo = playerSide.GetAllyUnit().weapon.GetMaxAmmo();
        
        actionChoices[0].SetActive(currentAmmo > 0);
        actionChoices[2].SetActive(currentAmmo < maxAmmo);
    }

    private void MovePanel(RectTransform panel, float target, ref Coroutine panelCoroutine) {
        if (panelCoroutine != null) StopCoroutine(panelCoroutine);
        panelCoroutine = StartCoroutine(DoMovePanel(panel, target, movePanelTime));
    }

    private IEnumerator DoMovePanel(RectTransform panel, float target, float duration) {
        float elapsedTime = 0f;
        Vector2 startPos = panel.anchoredPosition;
        Vector2 endPos = new Vector2(panel.anchoredPosition.x, target);

        while (elapsedTime < duration) {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            
            t = 1f - Mathf.Pow(1f - t, 3);
            
            panel.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            yield return null;
        }
        
        panel.anchoredPosition = endPos;
    }
}