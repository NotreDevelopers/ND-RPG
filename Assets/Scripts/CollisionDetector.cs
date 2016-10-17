using UnityEngine;
using System.Collections;

public class CollisionDetector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Impassable")
        {
            Debug.Log(coll.gameObject.name);


        }
    }
}
