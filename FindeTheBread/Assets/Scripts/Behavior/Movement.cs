using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public float moveSpeed = 5f;

    public Rigidbody2D rb;      //player rigidbody2d
    public Animator animator;

    Vector2 movement;
    
	// Update is called once per frame
	void Update () 
    {
        movement.x = Input.GetAxisRaw("Horizontal");    //Input
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal",movement.x);     //parameters in the animator that you adjust to play the right animation
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);  //the position the player move towards
    }
}
