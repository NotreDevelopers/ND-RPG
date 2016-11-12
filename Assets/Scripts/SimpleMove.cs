using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class SimpleMove : MonoBehaviour {
    #region Public Properties
    public float moveDist = 1; // Distance of a block of movement
    public float moveTime = 1; // Time for a block of movement (distance/time = speed)
    public float deadTime = 0.1f; // Amount of time spent restarting movement to reset collider

	public AudioSource audio;

    #endregion
    //---------------------------------------------------------------------------------
    #region Private Properties
    BoxCollider2D coll;
    SimpleAnimator anim;

    float dtime = 0; // Time spent moving (restarts every block of movement)
    bool moving = false;

    Vector2 moveDir;
    Vector2 oldPos;

    int impLayer; // Layer in Unity for impassible objects

    const float COLL_NEW_SCALE = 0.9f; // Rescales collider so that edges of collider don't touch other colliders
    const float MOVE_INPUT_THRESHOLD = 0.01f;

    #endregion
    //---------------------------------------------------------------------------------
    #region Runtime Functions

    void Start()
    {
        coll = this.GetComponent<BoxCollider2D>();
        anim = this.GetComponent<SimpleAnimator>();

        coll.size = new Vector2(coll.size.x * COLL_NEW_SCALE, coll.size.y * COLL_NEW_SCALE);

        dtime = deadTime; // Set so movement can happen immediately

        impLayer = LayerMask.NameToLayer("Impassable");

		audio = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        Move();
    }

    void OnTriggerEnter2D(Collider2D collided) // Standard Unity function for when this.collider enters another collider (as trigger)
    {
        if (collided.gameObject.layer == impLayer) // Checks if collider is part of impassable layer, and if so, rejects movement
        {
			audio.Play ();

            moving = false;
            dtime = 0;
            this.transform.position = oldPos;
            coll.offset = Vector2.zero;
        }
    }

    #endregion
    //---------------------------------------------------------------------------------
    #region Additional Functions

    /* Move: Utilizes both a boolean and time limitation to move the game object by increments.
     *      After input is received, the character moves in the input direction for a selected distance
     *      over a selected time.
     *          To check for collisions, the collider on this game object is moved into the space
     *      into which the game object will move. If a collider exists there, the game object won't move. */
    void Move()
    {
        if (moving == false) // While unit movement is happening, no new input can be given
        {
            if (dtime <= deadTime) // Due to the need to move the collider to and from the game object, a couple dead frames are needed
                dtime += Time.deltaTime;

            if (dtime > deadTime)
            {
                if (Mathf.Abs(Input.GetAxis("Horizontal")) > MOVE_INPUT_THRESHOLD) // Logic for horizontal movement
                {
                    moveDir = Vector2.right * Input.GetAxis("Horizontal");
                    moveDir.Normalize(); // Making sure movement input is unitary

                    if (moveDir.x > 0) // Plays animations on SimpleAnimator depending on which direction of movement
                        anim.Play("right");
                    else anim.Play("left");

                    moving = true; // Begin next section of logic
                    dtime = 0;
                }
                else if (Mathf.Abs(Input.GetAxis("Vertical")) > MOVE_INPUT_THRESHOLD) // Logic for vertical movement (mirrored from above logic)
                {
                    moveDir = Vector2.up * Input.GetAxis("Vertical");
                    moveDir.Normalize();

                    if (moveDir.y > 0)
                        anim.Play("up");
                    else anim.Play("down");

                    moving = true;
                    dtime = 0;
                }
            }
        }
        if (moving == true) // When movement begins
        {
            if (dtime == 0) // Grabs original position and places collider in direction of movement
            {
                oldPos = this.transform.position;
                coll.offset = moveDir;
            }
            if (dtime < moveTime) // If no collisions, while the change in time is less than movement time, increment distance moved
            {
                coll.offset = (moveDir) * (moveTime - dtime);

                this.transform.position = oldPos + moveDir * (dtime / moveTime);
            }
            else // When change in time is greater than movement time, set final position and restart collider and movement boolean
            {
                this.transform.position = oldPos + moveDir;
                coll.offset = Vector2.zero;
                moving = false;
            }

            dtime += Time.deltaTime; // Steps change in time by Unity's frame-time
        }
    }

    #endregion
}
