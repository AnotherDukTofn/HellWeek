using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SceneLoader : MonoBehaviour {
    [SerializeField] private SceneAsset gameplayScene;
    [SerializeField] private SceneAsset mainMenuScene;

    public void PlayGame() {
        SceneManager.LoadScene(gameplayScene.name);
    }
    
    public void BacktoMainMenu() {
        SceneManager.LoadScene(mainMenuScene.name);
    }

    public void QuitGame() {
        Application.Quit();
    }
}