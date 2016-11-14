using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class EventHandler : MonoBehaviour {
    #region Public Properties

    [System.Serializable]
    public struct CollPair
    {
        public Vector2 offset;
        public Vector2 size;
    }
    public float reshapeTime = 3.0f;

    public List<CollPair> Sequence;

    [HideInInspector]
    public bool runEvent = false;

    #endregion
    //---------------------------------------------------------------------------------
    #region Private Properties

    BoxCollider2D coll;

    float dt = 0.0f;
    int iterator = 0;

    #endregion
    //---------------------------------------------------------------------------------
    #region MonoBehavior Methods

    void Start()
    {
        coll = this.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        CheckEvent();
        ReshapeCollider();
    }

    #endregion
    //---------------------------------------------------------------------------------
    #region Additional Methods

    void CheckEvent()
    {
        if (runEvent && !GameObject.FindGameObjectWithTag("Player").GetComponent<SimpleMove>().getMoving())
        {
            Debug.Log("Event");

            if (this.gameObject.tag == "Enemy")
            {
                Debug.Log("- Enemy");
            }

            runEvent = false;
        }
    }

    void ReshapeCollider()
    {
        if (dt < reshapeTime)
        {
            dt += Time.deltaTime;
        }
        else
        {
            /* Here are two different implementations of changing the collider dimensions:
             * One changes the actual BoxCollider2D component values, and the other rotates the whole game object.
             * At this point, the advantage of the former is that it allows for more control by using a list of new
             * shapes to iterate through, while the advantage of the latter is simplicity. */

            iterator++;
            if (iterator >= Sequence.Count)
                iterator = 0;

            coll.size = Sequence[iterator].size;
            coll.offset = Sequence[iterator].offset;

            //this.gameObject.transform.Rotate(Vector3.forward, 90);

            dt = 0;
        }
    }
    #endregion
}
