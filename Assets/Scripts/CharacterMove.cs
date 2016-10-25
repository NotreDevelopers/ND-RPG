using UnityEngine;
using System.Collections;

public class CharacterMove : MonoBehaviour {
    #region Public Properties


    public Animator anim;
    public
    #endregion // Public Properties
    //---------------------------------------------------------------------
    void Start()
    {
        anim = this.GetComponent<Animator>();
	}
	
	void Update()
    {
        Move();
	}

    void Move()
    {
        if(Mathf.Abs(Input.GetAxis("Horizontal")) > 0.01f)
        {

        }
        else if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.01f)
        {

        }
    }
}
