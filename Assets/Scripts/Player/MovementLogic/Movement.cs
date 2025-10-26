using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.MovementLogic
{
    [RequireComponent(typeof(Rigidbody2D))] // Добавляй сюда используемые компоненты чтобы избежать ошибок в будущем
    public class Movement : MonoBehaviour
    {
        [Header("Движение влево-вправо")]
        [SerializeField] public float speed = 4f;
    
        [Header("Прыжок")]
        [SerializeField] public float jumpForce = 7f;
        [SerializeField] public int maxJumps = 2;
        
        [Header("Sprint")]
        [SerializeField] public float sprintSpeed = 10f;
        
        [Description("Время бега в секундах")]
        [SerializeField] public float sprintTime = 2f;
        //[SerializeField] private float targetZRotation; // Закомментил, т.к. бесполезно
    
        [Header("Логика отскока от стен")]
        [SerializeField] public float wallSlideSpeed = 2f;
        [SerializeField] public Vector2 wallJumpForce = new(5f, 10f);
        
        [Header("Collision checks")]
        [SerializeField] private Transform wallCheck;
        [SerializeField] public float wallCheckDistance = 0.5f;

        [SerializeField] private Transform groundCheck;
        [SerializeField] private Vector2 groundBoxSize = new(0.4f, 0.4f);

        [Header("Collision layers")]
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private LayerMask wallLayer;

        // Смотри, [SerializeField] и так означает что ты можешь получать доступ к переменным из инспектора,
        // а public - позволяет получать доступ И из инспектора, И из отдельных классов, что для тебя излишне.
        // Все переменные которые тебе нужны только в текущем scope оставь тут, то-есть замени public на private
        [SerializeField] private InputActionReference moveAction;
        [SerializeField] private InputActionReference jumpAction;
        [SerializeField] private InputActionReference sprintAction;
        
        private Rigidbody2D _rb;

        private bool _isSprinting;
        private bool _isGrounded;
        private bool _wasGrounded;
        private bool _isTouchingWall;
        
        private Vector2 _inputVector;
        private float _absDirection = 1f;
        private int _jumpCount;
        
        public event System.Action<bool> OnGroundedStateChanged;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _rb.freezeRotation = true;
        }

        private void OnEnable()
        {
            moveAction.action.Enable();
            jumpAction.action.Enable();
            sprintAction.action.Enable();
        
            moveAction.action.performed += OnMove;
            moveAction.action.canceled  += OnMove;
            jumpAction.action.performed += OnJump;
            sprintAction.action.performed += OnSprint;
        }
    
        private void OnDisable()
        {
            moveAction.action.performed -= OnMove;
            moveAction.action.canceled  -= OnMove;
            jumpAction.action.performed -= OnJump;
            sprintAction.action.performed -= OnSprint;

            moveAction.action.Disable();
            jumpAction.action.Disable();
            sprintAction.action.Disable();
        }

        public void FixedUpdate()
        {
            UpdateState();
            HandleMovement();
        }
        
        public void OnDrawGizmosSelected(){
            //проверка земли
            Gizmos.color = Color.red;
            Gizmos.DrawCube(groundCheck.position, groundBoxSize);
        
            //проверка стен
            Gizmos.color = Color.red;
            Gizmos.DrawRay(wallCheck.position, new Vector2(_absDirection, 0) * wallCheckDistance);
        }
        
        private void UpdateState()
        {
            if (_inputVector.x != 0 && !_isSprinting)
                _absDirection = Mathf.Sign(_inputVector.x);

            _wasGrounded = _isGrounded;
            _isGrounded = Physics2D.OverlapBox(groundCheck.position, groundBoxSize, 0f, groundLayer);
            _isTouchingWall = Physics2D.Raycast(wallCheck.position, new Vector2(_absDirection, 0), wallCheckDistance, wallLayer);

            if (_isGrounded != _wasGrounded)
                OnGroundedStateChanged?.Invoke(_isGrounded);
    
            if (_isGrounded && !_wasGrounded)
                _jumpCount = 0;
        }

        private void HandleMovement()
        {
            if (!_isSprinting)
                _rb.linearVelocity = new Vector2(_inputVector.x * speed, _rb.linearVelocity.y);

            if (_isTouchingWall && !_isGrounded && _rb.linearVelocity.y < 0)
                _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, -wallSlideSpeed);
        }
        
        private void OnMove(InputAction.CallbackContext context)
            => _inputVector = moveAction.action.ReadValue<Vector2>();

        private void OnJump(InputAction.CallbackContext context) 
        {
            if (_isTouchingWall && !_isGrounded) 
            {
                var jumpDirection = new Vector2(-_absDirection * wallJumpForce.x, wallJumpForce.y);
                _rb.linearVelocity = Vector2.zero;
                _rb.AddForce(jumpDirection, ForceMode2D.Impulse);
                _jumpCount = 1;
                return;
            }

            if (_isGrounded)
            {
                _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0f); 
                _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                _jumpCount = 1;
            }
            else if (_jumpCount < maxJumps)
            {
                _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0f); 
                _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                _jumpCount++;
            }
        }

        private void OnSprint(InputAction.CallbackContext context)
        {
            if (_isSprinting) return;
            StartCoroutine(SprintCoroutine());
        }
    
        private System.Collections.IEnumerator SprintCoroutine()
        {
            _isSprinting = true;
            _rb.linearVelocity = new Vector2(_absDirection * sprintSpeed, 0);
            //targetZRotation = _absDirection > 0 ? -15f : 15f;

            yield return new WaitForSeconds(sprintTime);

            _isSprinting = false;
            _rb.linearVelocity = new Vector2(0, _rb.linearVelocity.y);
            //targetZRotation = 0f;
        }
    }
}