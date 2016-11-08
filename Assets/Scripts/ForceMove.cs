using UnityEngine;
using System.Collections;

public class ForceMove : MonoBehaviour
{
    #region Public Properties
    public float moveDist = 1;
    public float moveTime = 1;

    #endregion
    //---------------------------------------------------------------------------------
    #region Private Properties
    Rigidbody2D rbdy;

    float dtime = 0;
    float speed;
    bool moving = false;

    Vector2 moveDir;

    const float MOVE_INPUT_THRESHOLD = 0.01f;

    #endregion
    //---------------------------------------------------------------------------------
    #region Runtime Functions

    void Start()
    {
        rbdy = this.GetComponent<Rigidbody2D>();

        rbdy.constraints = RigidbodyConstraints2D.FreezeRotation;

        speed = moveDist / moveTime;
    }

    void Update()
    {
        Move();
    }

    #endregion
    //---------------------------------------------------------------------------------
    #region Additional Functions

    void Move()
    {
        if (moving == false)
        {
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > MOVE_INPUT_THRESHOLD)
            {
                moveDir = Vector2.right * Input.GetAxis("Horizontal");
                moveDir.Normalize();

                moving = true;
                dtime = 0;
            }
            else if (Mathf.Abs(Input.GetAxis("Vertical")) > MOVE_INPUT_THRESHOLD)
            {
                moveDir = Vector2.up * Input.GetAxis("Vertical");
                moveDir.Normalize();

                moving = true;
                dtime = 0;
            }
        }
        else
        {
            if (dtime < moveTime)
            {
                rbdy.velocity = speed * moveDir;
            }
            else
            {
                rbdy.velocity = Vector2.zero;
                moving = false;
            }
            dtime += Time.deltaTime;
        }
    }

    #endregion
}
