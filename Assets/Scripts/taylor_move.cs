using UnityEngine;
using System.Collections;

public class taylor_move : MonoBehaviour {

	public float moveDist = 1;
	public float moveTime = 1;

	private bool isMoving = false;
	private Vector2 moveDir;
	private float dt = 0;
	private Vector2 oldPos;

	
	// Update is called once per frame
	void Update () {
		Move();
	}

	void Move()
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

			this.transform.Translate (moveDir);
		}




	}
}
