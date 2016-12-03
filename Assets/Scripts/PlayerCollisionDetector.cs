using UnityEngine;
using System.Collections;

public class PlayerCollisionDetector : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void onTriggerEnter2D(Collider2D other) {
		Debug.Log("PlayerCollisionDetector:: onTriggerEnter2D");
	}
}
