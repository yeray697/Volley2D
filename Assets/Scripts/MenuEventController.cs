using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuEventController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlayButtonClicked(){
		SceneManager.LoadScene ("game");
	}
	public void ConfigButtonClicked(){
	}

	public void ResumeGame() {
		Time.timeScale = 1.0F;
		GameObject.Find ("PauseCanvas").SetActive(false);
	}
	public void RestartGame() {
		Time.timeScale = 1.0F;
		SceneManager.LoadScene ("game");
	}
	public void QuitGame() {

		Time.timeScale = 1.0F;
		SceneManager.LoadScene ("menu_inicial");
	}
}
