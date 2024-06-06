using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D rb;
    public CapsuleCollider2D _capsuleCollider2D;
    private float gravityScaleAtStart;

    [Header("Move Info")]
    [SerializeField] public float MoveSpeed = 5f;
    [SerializeField] private float JumpForce = 7f;

    private bool isShoot;

    private bool climbing;

    private bool canMove = true;
    private bool canDoubleJump;
    private bool canWallslide;
    private bool canWallJump = true;
    private bool isWallSliding;
    private bool facingRight = true;
    private float MovingInput;
    private int facingDirection = 1;
    [SerializeField] private Vector2 wallJumpDirection;

    [Header("Ground Collision Info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask WhatIsGround;
    private bool isGround;

    [Header("Wall Collision Info")]
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    private bool isWallDetected;

    [Header("Shooting Info")]
    [SerializeField] private Transform arrowTransform;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private float arrowSpeed = 25f;

    // Start is called before the first frame update
    void Start()
    {
        //thêm sài tạm
        Time.timeScale = 1.0f;


        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        gravityScaleAtStart = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        CollisionCheck();
        FlipController();
        AnimatorController();
        HandleClimbing();
        
    }

    //Xử lí các cập nhật đến vật lý của Player
    private void FixedUpdate()
    {
        if (climbing)
        {
            var horizontalInput = Input.GetAxisRaw("Horizontal");
            var verticalInput = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector2(horizontalInput * MoveSpeed, verticalInput * MoveSpeed);
            return;
        }

        if (isGround)
        {
            canMove = true;
            canDoubleJump = true;
            canWallJump = true;
            canWallslide = false;
        }

        if (Input.GetAxis("Vertical") < 0)
        {
            canWallslide = false;
        }

        if (isWallDetected && canWallslide)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.2f);
        }
        else if (!isWallDetected)
        {
            isWallSliding = false;
            Move();
        }
    }
    
    //kiểm tra những InPut
    private void CheckInput()
    {
        if (climbing)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpButton();
        }

        if (canMove)
        {
            MovingInput = Input.GetAxis("Horizontal");
        }   
        if (Input.GetKeyDown(KeyCode.F) && isGround)
        {
            ShootButton();
        }
    }

    //điều kiện canMove để thực hiện di chuyển player
    private void Move()
    {
        if (canMove)
        {
            rb.velocity = new Vector2(MovingInput * MoveSpeed, rb.velocity.y);
        }
    }

    //Các điều kiện để thực hiện nhảy
    private void JumpButton()
    {
        if (climbing)
        {
            // Ignore jumping if the player is climbing
            return;
        }

        if (isWallSliding && canWallJump)
        {
            WallJump();
        }
        else if (isGround)
        {
            Jump();
        }
        else if (canDoubleJump)
        {
            canMove = true;
            canDoubleJump = false;
            Jump();
        }

        canWallslide = false;
    }

    //code nhảy
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, JumpForce);
    }

    //code nhảy tường 
    private void WallJump()
    {
        canMove = false;
        Vector2 direction = new Vector2(wallJumpDirection.x * -facingDirection, wallJumpDirection.y);
        rb.AddForce(direction, ForceMode2D.Impulse);
        canWallJump = false;
    }

    //Quay mặt Player
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    //Xử lí điều khiển quay mặt player
    private void FlipController()
    {
        if (!climbing && isGround && isWallDetected)
        {
            if (facingRight && MovingInput < 0)
            {
                Flip(); 
            }
            else if (!facingRight && MovingInput > 0)
            {
                Flip();
            }
        }

        if (MovingInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (MovingInput < 0 && facingRight)
        {
            Flip();
        }
    }

    //kiểm soát các animator qua code 
    private void AnimatorController()
    {
        bool isMoving = rb.velocity.x != 0;

        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isGrounded", isGround && !climbing); // đảm bảo chỉ tiếp đất nếu không leo
        anim.SetBool("isMoving", isMoving);//set chạy animation
        anim.SetBool("isWallSliding", isWallSliding);//set sìa tường animation
        anim.SetBool("isClimbing", climbing); // Ensure climbing state is set
        anim.SetBool("isShoot", isShoot);//set bắn tên animation
    }

    private void CollisionCheck()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, WhatIsGround) && !climbing; // Ensure not grounded if climbing
        isWallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, wallCheckDistance, WhatIsGround);

        if (!isGround && rb.velocity.y < 0)
        {
            canWallslide = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + Vector3.right * facingDirection * wallCheckDistance);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //chạy animation leo thang 
        if (collision.CompareTag("lander"))
        {
            rb.gravityScale = 0;
            rb.velocity = Vector2.zero;  // Stop any existing momentum
            climbing = true;
            anim.SetBool("isClimbing", true);
            anim.SetBool("isGrounded", false);  // Prevent grounded animations
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.CompareTag("lander"))
        {
            rb.gravityScale = gravityScaleAtStart;
            climbing = false;
            anim.SetBool("isClimbing", false);
        }
    }

    private void HandleClimbing()
    {
        if (climbing)
        {
            var leothang = Input.GetAxisRaw("Horizontal");
            var leothang2 = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector3(leothang * 1f, leothang2 * 3f, 0);
        }
    }
    private void ShootButton()
    {
        isShoot = true;
        GameObject arrow = Instantiate(arrowPrefab, arrowTransform.position, Quaternion.identity);
        Arrow arrowScript = arrow.GetComponent<Arrow>();
        arrowScript.Initialize(facingRight);
        int direction = facingRight ? 1 : -1;
        arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(arrowSpeed * direction, 0);

        // Reset isShoot animation sau 1 thời gian
        StartCoroutine(ResetShoot());
    }

    private IEnumerator ResetShoot()
    {
        yield return new WaitForSeconds(1f); // chỉnh sửa thời gian chờ để tiếp tục chạy lại animation
        isShoot = false;
    }
}