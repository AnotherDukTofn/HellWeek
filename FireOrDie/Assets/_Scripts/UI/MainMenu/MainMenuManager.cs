using UnityEngine;

public class MainMenuManager : MonoBehaviour {
    [SerializeField] private RectTransform optionPanel;
    [SerializeField] private RectTransform quitPanel;
    [SerializeField] private PanelMover mover;

    public void TurnOnOptions() {
        mover.Move(optionPanel, new Vector2(0, optionPanel.anchoredPosition.y));
        mover.Move(quitPanel, new Vector2(quitPanel.anchoredPosition.x, mover.hideY));
    }

    public void TurnOnQuit() {
        mover.Move(optionPanel, new Vector2(mover.hideX, optionPanel.anchoredPosition.y));
        mover.Move(quitPanel, new Vector2(quitPanel.anchoredPosition.x, 0));
    }

    public void BackMainMenu() {
        mover.Move(optionPanel, new Vector2(mover.hideX, optionPanel.anchoredPosition.y));
        mover.Move(quitPanel, new Vector2(quitPanel.anchoredPosition.x, mover.hideY));
    }
}
