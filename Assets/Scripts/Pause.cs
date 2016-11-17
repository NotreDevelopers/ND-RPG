using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour
{

	public GUISkin skin;

	private float gldepth = -0.5f;
	private float startTime = 0.1f;

	public Material mat;

	private long tris = 0;
	private long verts = 0;
	private float savedTimeScale;

	private bool showfps;
	private bool showtris;
	private bool showvtx;
	private bool showfpsgraph;

	public Color lowFPSColor = Color.red;
	public Color highFPSColor = Color.green;

	public int lowFPS = 30;
	public int highFPS = 50;

	public GameObject start;

	public string url = "unity.html";

	public Color statColor = Color.yellow;

	public GameObject Menu;


	public Texture[] crediticons;

	public enum Page {
		None,Main,Options,Credits
	}

	private Page currentPage;

	private float[] fpsarray;
	private float fps;

	private int toolbarInt = 0;



	void Start() {
		fpsarray = new float[Screen.width];
		Time.timeScale = 1;
		//Comment the next line out if you don't want it to pause right at the beginning
		//PauseGame();
	}

	void OnPostRender() {
		if (showfpsgraph && mat != null) {
			GL.PushMatrix ();
			GL.LoadPixelMatrix();
			for (var i = 0; i < mat.passCount; ++i)
			{
				mat.SetPass(i);
				GL.Begin( GL.LINES );
				for (int x=0; x < fpsarray.Length; ++x) {
					GL.Vertex3(x, fpsarray[x], gldepth);
				}
				GL.End();
			}
			GL.PopMatrix();
			ScrollFPS();
		}
	}

	void ScrollFPS() {
		for (int x = 1; x < fpsarray.Length; ++x) {
			fpsarray[x-1]=fpsarray[x];
		}
		if (fps < 1000) {
			fpsarray[fpsarray.Length - 1]=fps;
		}
	}


	void LateUpdate () {
		if (showfps || showfpsgraph) {
			FPSUpdate();
		}

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

	void FPSUpdate() {
		float delta = Time.smoothDeltaTime;
		if (!IsGamePaused() && delta !=0.0) {
			fps = 1 / delta;
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

	void OnApplicationPause(bool pause) {
		if (IsGamePaused()) {
			AudioListener.pause = true;
		}
	}
}
