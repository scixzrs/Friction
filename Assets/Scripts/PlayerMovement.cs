using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private CapsuleCollider2D playerCol;
    [SerializeField] private LayerMask ground;
    [SerializeField] private LayerMask obj;
    [SerializeField] private LayerMask bg;
    bool facingRight = true;
    float moveSpeed = 7.0f;
    float jumpForce = 12.0f;
    public Animator animator;
    private float dirX = 0f;

    private enum MovementState { Idle, Walk, Jump, Fall}

    // Start is called before the first frame update
    private void Start()
    {   //getting rigidbody component of player and saving it
        rb = GetComponent<Rigidbody2D>();
        playerCol = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxis("Horizontal");       //clamping axis values (-1 to 1)
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        if (dirX < 0 && facingRight)
        {
            flip();
        }
        else if (dirX > 0 && !facingRight)
        {
            flip();
        }

        UpdateAnimState();
    }

    private void UpdateAnimState()
    {
        MovementState state;
        if (dirX>0 || dirX<0)
        {
            state = MovementState.Walk;
        }
        else
        {
            state = MovementState.Idle;
        }

        if (rb.velocity.y > 0.1f)
        {
            state = MovementState.Jump;
        }
        else if (rb.velocity.y < -0.1f)
        {
            state = MovementState.Fall;
        }
  
        animator.SetInteger("state", (int)state);
    }  
    private Boolean IsGrounded()
    {
            return Physics2D.BoxCast(playerCol.bounds.center, playerCol.bounds.size, 0.0f, Vector2.down, 0.1f, ground) ||
            Physics2D.BoxCast(playerCol.bounds.center, playerCol.bounds.size, 0.0f, Vector2.down, 0.1f, obj) || 
            Physics2D.BoxCast(playerCol.bounds.center, playerCol.bounds.size, 0.0f, Vector2.down, 0.1f, bg);
    }

    void flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

}
