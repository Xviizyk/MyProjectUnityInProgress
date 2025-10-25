using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveInput;
    [SerializeField] private float lastDirection = 1f;
    [SerializeField] private bool isDashing = false;
    [SerializeField] private float dashTimer = 0f;
    [Header("Движение влево-вправо")]
    [SerializeField] public float speed = 4f;
    [Header("Прыжок")]
    [SerializeField] public float jumpForce = 7f;
    [SerializeField] public int maxJumps = 2;
    [SerializeField] private int jumpCount;
    [Header("Dash")]
    [SerializeField] public float dashSpeed = 10f;
    [SerializeField] public float dashTime = 0.2f;
    [SerializeField] private float targetZRotation = 0f;
    [Header("Логика отскока от стен")]
    [SerializeField] public float wallSlideSpeed = 2f;
    [SerializeField] public float wallJumpForceX = 5f;
    [SerializeField] public float wallJumpForceY = 10f;
    [SerializeField] public Transform wallCheck;
    [SerializeField] public float wallCheckDistance = 0.5f;
    [SerializeField] public LayerMask whatIsWall;
    [SerializeField] private bool isTouchingWall;
    [Header("Короче, эта часть является конфиг системы (ну или логики) проверки того, что песронаж (ну или игрок) находится на слое под названием граунд (тзачем я все это писал?)")]
    [SerializeField] public Transform groundCheck;
    [SerializeField] public float groundRadius = 0.2f;
    [SerializeField] public LayerMask groundLayer;
    [SerializeField] public bool isGrounded;
    
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }
    
    void Update(){
        moveInput = Input.GetAxisRaw("Horizontal");
        if (moveInput != 0 && !isDashing){
            lastDirection = Mathf.Sign(moveInput);
        }
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
        if (isGrounded){
            jumpCount = 0;
        }
        isTouchingWall = Physics2D.Raycast(wallCheck.position, new Vector2(lastDirection, 0), wallCheckDistance, whatIsWall);
        if (Input.GetKeyDown(KeyCode.Space)){
            if (isTouchingWall && !isGrounded){
                Vector2 jumpDirection = new Vector2(-lastDirection * wallJumpForceX, wallJumpForceY);
                rb.linearVelocity = Vector2.zero;
                rb.AddForce(jumpDirection, ForceMode2D.Impulse);
                jumpCount = 1;
            }
            else{
                if (isGrounded || jumpCount < maxJumps){
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                    if(!isTouchingWall){
                        jumpCount++;
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing){
            isDashing = true;
            dashTimer = dashTime;
            float dashDirection = lastDirection;
            rb.linearVelocity = new Vector2(dashDirection * dashSpeed, 0);
            targetZRotation = (dashDirection > 0) ? -15f : 15f;
        }
        if (isDashing){
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0){
                isDashing = false;
                rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
                targetZRotation = 0f;
            }
        }
    }
    
    void FixedUpdate(){
        if (!isDashing){
            rb.linearVelocity = new Vector2(moveInput*speed, rb.linearVelocity.y);
        }
        if (isTouchingWall && !isGrounded && rb.linearVelocity.y < 0){
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -wallSlideSpeed);
        }
    }
    
    void OnDrawGizmosSelected(){
        //проверка земли
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
        //проверка стен
        Gizmos.color = Color.red;
        Gizmos.DrawRay(wallCheck.position, new Vector2(lastDirection, 0) * wallCheckDistance);
    }
}