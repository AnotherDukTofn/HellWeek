using System.Collections;
using UnityEngine;

public class ChoosingUI : MonoBehaviour {
    [Header("References")]
    [SerializeField] private ChoosingManager choosingManager;
    [SerializeField] private RectTransform actionPanel;
    [SerializeField] private RectTransform targetPanel;
    [SerializeField] private Playside playerSide;
    
    [Header("Move Panel Settings")]
    [SerializeField] private float movePanelTime;
    [SerializeField] private float moveDownPos;
    [SerializeField] private float moveUpPos;
    
    //cached coroutine
    private Coroutine _actionPanelCoroutine;
    private Coroutine _targetPanelCoroutine;

    public void NavigateToTargetPanel() {
        ResetTargetPanel();
        MovePanel(actionPanel, moveDownPos, ref _actionPanelCoroutine);
        MovePanel(targetPanel, moveUpPos, ref _targetPanelCoroutine);
    }

    public void NavigateToActionPanel() {
        MovePanel(targetPanel, moveDownPos, ref _targetPanelCoroutine);
        MovePanel(actionPanel, moveUpPos, ref _actionPanelCoroutine);
    }

    public void TurnOffChoosing() {
        MovePanel(actionPanel, moveDownPos, ref _actionPanelCoroutine);
        MovePanel(targetPanel, moveDownPos, ref _targetPanelCoroutine);
    }

    public void ResetTargetPanel() {
        if (choosingManager.actionType == ChoosingManager.ActionType.Dodge) {
            for (int i = 0; i < targetPanel.transform.childCount; i++) {
                var targetChoice = targetPanel.transform.GetChild(i).gameObject;
                bool occupied = playerSide.Cells[i].IsOccupied();
                targetChoice.SetActive(!occupied);
            }
        }
        else {
            foreach (Transform child in targetPanel.transform) {
                child.gameObject.SetActive(true);
            }
        }
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