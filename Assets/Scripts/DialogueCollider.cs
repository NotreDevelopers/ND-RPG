using UnityEngine;
using System.Collections;

public class DialogueCollider : MonoBehaviour {

	public string Dialogue;
	private DialogueManager dObject;
	private GameObject player;

	// Use this for initialization
	void Start () {
		dObject = FindObjectOfType<DialogueManager>(); //<> is for versatility of multiple sprites/objects
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate () // called every 30 to 60 seconds by physics engine
	{
		//Debug.Log(Vector2.Distance(player.transform.position, this.transform.position));
		if (Vector2.Distance (player.transform.position, this.transform.position) <= 1.2f) {
			dObject.canDisplay = true;
		} else {
			dObject.canDisplay = false;
		}
	}

	void onTriggerEnter2D(Collider2D other) //onTriggerStay accounts for every moment in the zone (so that space can be hit anytime, rather than as soon as you enter the zone)
	{
		Debug.Log ("onTriggerEnter2D:: entered");
		//conditional works by having the name of the player, "Test Main" be in the space of object
		if (other.gameObject.tag == "Player") //checking if player is entering object space 
		{ 
			dObject.canDisplay = true;
		}
	}

	void onTriggerExit2D(Collider2D other)
	{
		Debug.Log ("onTriggerExit2D:: entered");
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
