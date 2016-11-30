using UnityEngine;
using System.Collections;

public class DialogueCollider : MonoBehaviour {

	public string Dialogue;
	private DialogueManager dObject;

	// Use this for initialization
	void Start () {
		dObject = FindObjectOfType<DialogueManager>(); //<> is for versatility of multiple sprites/objects
		 
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void onTriggerEnter2D(Collider2D other) //onTriggerStay accounts for every moment in the zone (so that space can be hit anytime, rather than as soon as you enter the zone)
	{
		//conditional works by having the name of the player, "Test Main" be in the space of object
		if (other.gameObject.tag == "Player") //checking if player is entering object space 
		{ 
			dObject.canDisplay = true;
		}
	}

	void onTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") //checking if player is exiting object space 
		{ 
			dObject.canDisplay = false;
		}


	}

	void ShowDialogue()
	{
		if (Input.GetKeyUp(KeyCode.Space)) 
		{
			dObject.ShowBox(Dialogue);
		}
	}
}
