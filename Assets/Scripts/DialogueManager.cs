using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogueManager : MonoBehaviour {
		
	public GameObject dBox; 
	public Text dText;
	public bool dialogActive = false; 
	public GameObject boxText;
	private BoxCollider2D dialogueZone;
	private GameObject player;

	public bool canDisplay;

	// Use this for initialization
	void Start () {
		dBox.SetActive (false);
		player = GameObject.FindGameObjectWithTag ("Player");

		canDisplay = false;
		//dialogueZone = GameObject.FindGameObject.GetComponent<BoxCollider2D>();
		//what i need: 
		/* if the player's collision is colliding with the dialogue collider. 
		 * handle for is touching component of the colliders to be true
		 * Then set text to hi, and make sure that it's versatile with other objects and unique texts*/ 
	}
	
	// Update is called once per frame
	void Update () {
		if (dialogActive == false && Input.GetKeyDown(KeyCode.Space) && canDisplay)
		{
			dBox.SetActive(true); //deactivate box
			dialogActive = true;
			dText.GetComponent<Text>().text = "Hi!";

		}
		else if (dialogActive == true && Input.GetKeyDown(KeyCode.Space))
		{
			dBox.SetActive(false); //deactivate box
			dialogActive = false;
		}
	}

	// For other text to show
	public void ShowBox(string Dialogue)
	{
		if (!canDisplay) {
			dBox.SetActive(false); //deactivate box
			dialogActive = false;
			return;
		}
		dText.text = Dialogue;
		dialogActive = true;
		dBox.SetActive (true);
	}

	public void DisplayDialogue(int index)
	{
		
	}
}
