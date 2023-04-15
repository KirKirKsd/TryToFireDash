using UnityEngine;

public class PauseMenuScript : MonoBehaviour {

	private bool isPaused;

	public GameObject PauseMenuUI;
	public GameObject SettingsUI;

    private void Start() {
		Cursor.visible = false;
    }

    public void Switch() {
		switch (isPaused) {
			case true:
				Resume();
				break;
			case false:
				Pause();
				break;
        }
		isPaused = !isPaused;
	} 

	
	private void Resume() {
		PauseMenuUI.SetActive(false);
		SettingsUI.SetActive(false);
		Cursor.visible = false;
		Time.timeScale = 1f;
    }

	private void Pause() {
		PauseMenuUI.SetActive(true);
		Cursor.visible = true;
		Time.timeScale = 0f;
    }

	public void Settings() {
		PauseMenuUI.SetActive(false);
		SettingsUI.SetActive(true);
    }

	public void BackToPause() {
		PauseMenuUI.SetActive(true);
		SettingsUI.SetActive(false);
	}

	public void Exit() {
		Application.Quit();
    }

}
