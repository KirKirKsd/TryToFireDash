using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class PauseMenu : MonoBehaviour {

    private bool isPaused;
    
    private UIDocument pauseMenuDoc;
    private VisualElement pauseMenuRoot;

    private VisualElement pauseMenuPanel;

    private void Start() {
        pauseMenuDoc = GetComponent<UIDocument>();
        pauseMenuRoot = pauseMenuDoc.rootVisualElement;

        pauseMenuPanel = pauseMenuRoot.Q<VisualElement>("Panel");
        
        pauseMenuRoot.Q<Button>("Resume").clicked += Switch;
        pauseMenuRoot.Q<Button>("Settings").clicked += Settings;
        pauseMenuRoot.Q<Button>("Exit").clicked += Exit;
    }

    public void Switch() {
        switch (isPaused) {
            case true:
                Pause();
                break;
            case false:
                Resume();
                break;
        }

        isPaused = !isPaused;
    }
    
    private void Resume() {
        Cursor.visible = false;
        pauseMenuPanel.style.visibility = Visibility.Hidden;
        Time.timeScale = 1f;
    }

    private void Pause() {
        Cursor.visible = true;
        pauseMenuPanel.style.visibility = Visibility.Visible;
        Time.timeScale = 0f;
    }

    private void Settings() {
        
    }
    
    private static void Exit() {
        Application.Quit();
    }

}
