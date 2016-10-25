using UnityEngine;
using System.Collections;

public class SimpleMove : MonoBehaviour {
    #region Public Properties
    public float moveDist = 1;
    public float moveTime = 1;
    public float deadTime = 0.1f;

    #endregion
    //---------------------------------------------------------------------------------
    #region Private Properties
    BoxCollider2D coll;
    SimpleAnimator anim;

    float dtime = 0;
    bool moving = false;

    Vector2 moveDir;
    Vector2 oldPos;

    int impLayer;

    const float COLL_NEW_SCALE = 0.9f;
    const float MOVE_INPUT_THRESHOLD = 0.01f;

    #endregion
    //---------------------------------------------------------------------------------
    #region Runtime Functions

    void Start()
    {
        coll = this.GetComponent<BoxCollider2D>();
        anim = this.GetComponent<SimpleAnimator>();

        coll.size = new Vector2(coll.size.x * COLL_NEW_SCALE, coll.size.y * COLL_NEW_SCALE);

        dtime = deadTime;

        impLayer = LayerMask.NameToLayer("Impassable");
	}
	
	void Update()
    {
        Move();
	}

    void OnTriggerEnter2D(Collider2D collided)
    {
        if (collided.gameObject.layer == impLayer)
        {
            moving = false;
            dtime = 0;
            this.transform.position = oldPos;
            coll.offset = Vector2.zero;
        }
    }

    #endregion
    //---------------------------------------------------------------------------------
    #region Additional Functions

    void Move()
    {
        if (moving == false)
        {
            if (dtime <= deadTime)
                dtime += Time.deltaTime;

            if (dtime > deadTime)
            {
                if (Mathf.Abs(Input.GetAxis("Horizontal")) > MOVE_INPUT_THRESHOLD)
                {
                    moveDir = Vector2.right * Input.GetAxis("Horizontal");
                    moveDir.Normalize();

                    coll.offset = Vector2.zero;
                    moving = true;
                    dtime = 0;
                }
                else if (Mathf.Abs(Input.GetAxis("Vertical")) > MOVE_INPUT_THRESHOLD)
                {
                    moveDir = Vector2.up * Input.GetAxis("Vertical");
                    moveDir.Normalize();

                    coll.offset = Vector2.zero;
                    moving = true;
                    dtime = 0;
                }
            }
        }
        if (moving == true)
        {
            if (dtime == 0)
            {
                oldPos = this.transform.position;
                coll.offset = moveDir;
            }
            if (dtime < moveTime)
            {
                coll.offset = (moveDir) * (moveTime - dtime);

                this.transform.position = oldPos + moveDir * (dtime / moveTime);
            }
            else
            {
                this.transform.position = oldPos + moveDir;
                coll.offset = Vector2.zero;
                moving = false;
            }

            dtime += Time.deltaTime;
        }
    }

    bool CheckCollision(Vector2 dir)
    {
        bool isTouching = false;
        coll.offset = dir;

        isTouching = coll.IsTouchingLayers(impLayer);
        //coll.offset = Vector2.zero;

        return isTouching;
    }

    #endregion
}
