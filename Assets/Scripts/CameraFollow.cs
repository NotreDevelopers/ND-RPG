using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    private GameObject following;

    void Awake()
    {
        following = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        this.transform.position = following.transform.position + Vector3.forward * (-10);
    }
}
