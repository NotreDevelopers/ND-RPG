using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public GameObject following;
	
    private float dt = 0.5f;
    private float followT = 1;

    private Vector3 folPos;

	void Update ()
    {
        if (folPos != following.transform.position + Vector3.forward * (-10))
        {
            folPos = following.transform.position + Vector3.forward * (-10);
            dt = 0.5f;
        }

        this.transform.position = Vector3.Lerp(this.transform.position, folPos, dt/followT);

        if (dt < followT)
            dt += Time.deltaTime / 100;

        //this.transform.position = following.transform.position + Vector3.forward * (-10);
    }
}
