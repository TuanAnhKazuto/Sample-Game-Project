using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D rb;

    [Header("move info")]
    [SerializeField]public float MoveSpeed = 5f;
    [SerializeField]private float JumpForce =7;

    private bool canMove = true;

    private bool canDoubleJump ;
    private bool canWallslide;
    private bool isWallSliding;
   
    private bool facingRight = true;
    private float MovingInPut;

    [Header("Ground collision info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask WhatIsGround;
                     private bool isGround;
    [Header("Wall collision info")]
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    private bool isWallDetected;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       CheckInput();

        

        CollisionCheck();
        FlipController();
        AnimatorController();
    }

    private void Jump()
    {
        
        
        rb.velocity = new Vector2(rb.velocity.x, JumpForce);
    }


    private void FixedUpdate()
    {
        if (isGround)
        {
            canDoubleJump = true;
        }

        if (isWallDetected && canWallslide)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y *0.2f);
        }
        else
        {
            isWallSliding = false;
            Move();
        }
    }

    private void CheckInput()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpButton();
        }
        if (canMove)
        {
            MovingInPut = Input.GetAxis("Horizontal");
        }
    }
    private void Move()
    {
        if (canMove)
        {
            rb.velocity = new Vector2(MovingInPut * MoveSpeed, rb.velocity.y);
        }
    }
    private void JumpButton()
    {
        if (isGround)
        {
            Jump();
        }
        else if (canDoubleJump)
        {
            canDoubleJump = false;
            Jump();
        }
    }
    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    private void FlipController()
    {
        if(isGround && isWallDetected)
        {
            if (facingRight && MovingInPut < 0)
            {
                Flip();
            }
            else if (!facingRight && MovingInPut > 0)
            {
                Flip();
            }
        }

        if (rb.velocity.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (rb.velocity.x < 0 && facingRight)
        {
            Flip();
        }
    }

    private void AnimatorController()
    {
        bool isMoving = rb.velocity.x != 0;

        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isGrounded", isGround);
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isWallSliding", isWallSliding);
    }
    private void CollisionCheck()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, WhatIsGround);
        isWallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance, WhatIsGround);

        if (!isGround && rb.velocity.y < 0)
        {
            canWallslide = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        Gizmos.DrawLine(wallCheck.position, new Vector3 (wallCheck.position.x + wallCheckDistance, 
                                                         wallCheck.position.y, 
                                                         wallCheck.position.z));
    }
}
