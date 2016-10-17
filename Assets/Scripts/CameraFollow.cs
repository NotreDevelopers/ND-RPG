using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public GameObject following;
	
	void Update ()
    {
        this.transform.position = following.transform.position + Vector3.forward * (-10);
	}
}
