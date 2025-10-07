using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveInput;
    private float lastDirection = 1f;
    private bool isDashing = false;
    private float dashTimer = 0f;

    [Header("Горизонтальное движение")]
    public float speed = 4f;

    [Header("Параметры прыжка")]
    public float jumpForce = 7f;
    public int maxJumps = 2;
    private int jumpCount;

    [Header("Параметры рывка (дэша)")]
    public float dashSpeed = 10f;
    public float dashTime = 0.2f;

    [Header("Взаимодействие со стенами")]
    public float wallSlideSpeed = 2f;
    public float wallJumpForceX = 5f;
    public float wallJumpForceY = 10f;
    public Transform wallCheck;
    public float wallCheckDistance = 0.5f;
    public LayerMask whatIsWall;
    private bool isTouchingWall;

    [Header("Проверка земли")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public bool isGrounded;

    [Header("Поворот спрайта")]
    public Transform objectSprite;
    public float rotationSpeed = 10f;
    private float targetZRotation = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if (moveInput != 0 && !isDashing)
        {
            lastDirection = Mathf.Sign(moveInput);
        }

        GroundCheck();
        WallCheck();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isTouchingWall && !isGrounded)
            {
                WallJump();
            }
            else
            {
                NormalJump();
            }
        }

        HandleDash();
        HandleSpriteRotation();
    }

    void FixedUpdate()
    {
        if (!isDashing)
        {
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        }

        if (isTouchingWall && !isGrounded && rb.velocity.y < 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
        }
    }

    private void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (isGrounded)
        {
            jumpCount = 0;
        }
    }

    private void WallCheck()
    {
        isTouchingWall = Physics2D.Raycast(wallCheck.position, new Vector2(lastDirection, 0), wallCheckDistance, whatIsWall);
    }

    private void NormalJump()
    {
        if (isGrounded || jumpCount < maxJumps)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
        }
    }

    private void WallJump()
    {
        Vector2 jumpDirection = new Vector2(-lastDirection * wallJumpForceX, wallJumpForceY);
        rb.velocity = Vector2.zero;
        rb.AddForce(jumpDirection, ForceMode2D.Impulse);
        jumpCount = 1;
    }

    private void HandleDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing)
        {
            isDashing = true;
            dashTimer = dashTime;

            float dashDirection = lastDirection;
            rb.velocity = new Vector2(dashDirection * dashSpeed, 0);
            targetZRotation = (dashDirection > 0) ? -15f : 15f;
        }

        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
            {
                isDashing = false;
                rb.velocity = new Vector2(0, rb.velocity.y);
                targetZRotation = 0f;
            }
        }
    }

    private void HandleSpriteRotation()
    {
        if (objectSprite != null)
        {
            if (!isDashing)
            {
                if (moveInput > 0) objectSprite.localScale = new Vector3(1, 1, 1);
                else if (moveInput < 0) objectSprite.localScale = new Vector3(-1, 1, 1);
            }

            Quaternion targetRot = Quaternion.Euler(0, 0, targetZRotation);
            objectSprite.localRotation = Quaternion.Lerp(objectSprite.localRotation, targetRot, rotationSpeed * Time.deltaTime);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }

        if (wallCheck != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(wallCheck.position, new Vector2(lastDirection, 0) * wallCheckDistance);
        }
    }
}