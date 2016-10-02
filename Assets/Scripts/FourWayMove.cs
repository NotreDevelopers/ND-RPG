using UnityEngine;
using System.Collections;

public class FourWayMove : MonoBehaviour {

    public float moveDist = 1;
    public float moveTime = 1;

    private bool isMoving = false;
    private Vector2 moveDir;
    private float dt = 0;
    private Vector2 oldPos;
	
	void Update ()
    {
        Move();
	}

    void Move()
    {
        if (!isMoving)
        {
            if (Input.GetAxisRaw("Horizontal") > 0.1 || Input.GetAxisRaw("Horizontal") < -0.1)
            {
                moveDir = Vector2.right * Input.GetAxisRaw("Horizontal");
                moveDir.Normalize();
                isMoving = true;
            }
            else if (Input.GetAxisRaw("Vertical") > 0.1 || Input.GetAxisRaw("Vertical") < -0.1)
            {
                moveDir = Vector2.up * Input.GetAxisRaw("Vertical");
                moveDir.Normalize();
                isMoving = true;
            }
        }

        if (isMoving)
        {
            this.transform.position = Vector2.Lerp(oldPos, oldPos + moveDist * moveDir, dt / moveTime);
            dt += Time.deltaTime;

            if (dt > moveTime)
            {
                dt = 0;
                isMoving = false;
                this.transform.position = oldPos + moveDist * moveDir;
                oldPos = this.transform.position;
            }
        }
    }
}
