using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour
{
	private float startTime = 0.1f;

	private float savedTimeScale;

	private bool showfps;
	private bool showfpsgraph;

	public Color lowFPSColor = Color.red;
	public Color highFPSColor = Color.green;

	public GameObject start;

	public Color statColor = Color.yellow;

	public GameObject Menu;


	public Texture[] crediticons;

	public enum Page {
		None,Main,Options,Credits
	}

	private Page currentPage;

	void Start() {
		Time.timeScale = 1;
        Menu.SetActive(false);
        //Comment the next line out if you don't want it to pause right at the beginning
        //PauseGame();
	}

	void LateUpdate () {
		if (Input.GetKeyDown("escape")) 
		{
			switch (currentPage) 
			{
			case Page.None: 
				PauseGame(); 
				break;

			case Page.Main: 
				if (!IsBeginning()) 
					UnPauseGame(); 
				break;

			default: 
				currentPage = Page.Main;
				break;
			}
		}
	}

	bool IsBeginning() {
		return (Time.time < startTime);
	}

	void PauseGame() {

		Menu.SetActive (true);
		savedTimeScale = Time.timeScale;
		Time.timeScale = 0;
		AudioListener.pause = true;

		currentPage = Page.Main;
	}

	void UnPauseGame() {

		Menu.SetActive (false);
		Time.timeScale = savedTimeScale;
		AudioListener.pause = false;
		currentPage = Page.None;

		if (IsBeginning() && start != null) {
			start.SetActive(true);
		}
	}

	bool IsGamePaused() {
		return (Time.timeScale == 0);
	}
}
