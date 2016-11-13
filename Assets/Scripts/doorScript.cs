using UnityEngine;
using System.Collections;

public class doorScript : MonoBehaviour {

    // Door ID within GameManager door dictionaries
    public int doorID;

    // Positon that player is placed upon entering through door
    public float entranceOffsetX;
    public float entranceOffsetY;

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Door collision detected!");

        if (other.gameObject.tag == "Player")
        {
            GameManagerScript.instance.loadSceneFromDoor(doorID);
        }
    }
}
