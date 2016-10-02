using UnityEngine;
using System.Collections;

public class CollisionDetector : MonoBehaviour {

    public LayerMask collideLayer = 7;

    private bool messageOut = false;
    private BoxCollider2D colliderPlayer;

	// Use this for initialization
	void Start ()
    {
        colliderPlayer = this.GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (colliderPlayer.IsTouchingLayers(collideLayer) && messageOut == false)
        {
            Debug.Log("Touching");
            messageOut = true;
        }
        else
        {
            if(!colliderPlayer.IsTouchingLayers(collideLayer) && messageOut == true)
            {
                Debug.Log("Not touching");
                messageOut = false;
            }
        }
	}
}
