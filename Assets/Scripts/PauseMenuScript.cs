using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour {

	private bool isPaused;

	public GameObject PauseMenuUI;
	public GameObject SettingsUI;
	public Upgrades upgradesScript;
	private PlayerMotor playerMotorScript;

    private void Start() {
	    Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		playerMotorScript = GetComponent<PlayerMotor>();
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
		GameObject.FindGameObjectWithTag("Player").GetComponent<MusicPlayer>().controlsUI.SetActive(false);
		if (!upgradesScript.canUpgrade) {
			playerMotorScript.walk.Enable();
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
			Time.timeScale = 1f;
		}
    }

	private void Pause() {
		PauseMenuUI.SetActive(true);
		GameObject.FindGameObjectWithTag("Player").GetComponent<MusicPlayer>().controlsUI.SetActive(true);
		playerMotorScript.walk.Disable();
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.Confined;
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

	public void Menu() {
		GameObject.FindGameObjectWithTag("Bonfire").GetComponent<Bonfire>().Death();
	}
	
	public void Exit() {
		Application.Quit();
    }

}
