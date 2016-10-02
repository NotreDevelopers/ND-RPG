using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

    public float moveDist = 1;
    public float moveTime = 1;

    private bool isMoving = false;
    private Vector2 moveDir;
    private float dt = 0;
    private Vector2 oldPos;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Move1();	
	}

    void Move1()
    {
        {
            {
                moveDir += Vector2.right * Input.GetAxisRaw("Horizontal");
                moveDir.Normalize();
                isMoving = true;
            }
            {
                moveDir += Vector2.up * Input.GetAxisRaw("Vertical");
                moveDir.Normalize();
                isMoving = true;
            }
            this.transform.Translate(moveDir);
        }
    }
}
