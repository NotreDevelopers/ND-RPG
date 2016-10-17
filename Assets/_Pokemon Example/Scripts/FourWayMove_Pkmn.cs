using UnityEngine;
using System.Collections;

public class FourWayMove_Pkmn : MonoBehaviour
{

    public float moveDist = 1;
    public float moveTime = 1;

    int impassableLayer = 8;
    bool isMoving = false;
    bool tryMove = false;
    bool canMove = true;
    Vector2 moveDir;
    float dt = 0;
    Vector2 oldPos;

    Animator anim;

    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == impassableLayer)
        {
            Debug.Log(col.gameObject.name);
            canMove = false;
            anim.ResetTrigger("up");
            anim.ResetTrigger("down");
            anim.ResetTrigger("right");
            anim.ResetTrigger("left");
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        canMove = true;
    }

    void FixedUpdate()
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
                isMoving = TestMove(moveDir);

                if (moveDir.x > 0.1)
                {
                    anim.SetTrigger("right");
                }
                else if (moveDir.x < -0.1)
                {
                    anim.SetTrigger("left");
                }
            }
            else if (Input.GetAxisRaw("Vertical") > 0.1 || Input.GetAxisRaw("Vertical") < -0.1)
            {
                moveDir = Vector2.up * Input.GetAxisRaw("Vertical");
                moveDir.Normalize();
                isMoving = TestMove(moveDir);

                if (moveDir.y > 0.1)
                {
                    anim.SetTrigger("up");
                }
                else if (moveDir.y < -0.1)
                {
                    anim.SetTrigger("down");
                }
            }
        }

        if (isMoving)
        {
            anim.SetBool("isMoving", true);
            this.transform.position = Vector2.Lerp(oldPos, oldPos + moveDist * moveDir, dt / moveTime);
            dt += Time.deltaTime;

            if (dt > moveTime)
            {
                dt = 0;
                isMoving = false;
                this.transform.position = oldPos + moveDist * moveDir;
                oldPos = this.transform.position;
                if (Input.GetAxisRaw("Vertical") > 0.1 || Input.GetAxisRaw("Vertical") < -0.1 || Input.GetAxisRaw("Horizontal") > 0.1 || Input.GetAxisRaw("Horizontal") < -0.1)
                    anim.SetBool("isMoving", true);
                else anim.SetBool("isMoving", false);
            }
        }
    }

    bool TestMove(Vector2 moveDirection)
    {
        if (tryMove)
        {
            this.GetComponent<Collider2D>().offset = moveDirection * moveDist;
            tryMove = false;
            return false;
        }
        else
        {
            this.GetComponent<Collider2D>().offset = Vector2.zero;
            tryMove = true;
        }

        if (canMove)
            return true;

        return false;
    }
}
