using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Move Speed")]
    [SerializeField] private float topSpeed = 6f;
    [SerializeField] private float acceleration = 25f;

    [Header("Jump Parameters")]
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float coyoteTime = 0.15f;
    [SerializeField] private float jumpBuffer = 0.15f;
    [SerializeField] private float jumpCutMltp = 0.5f;
    [SerializeField] private float fallMltp = 2.5f;
    [SerializeField] private float lowJumpMltp = 2f;
    [SerializeField] private float maxFallSpeed = -20f;

    [Header("Ground Info")]
    [SerializeField] private LayerMask ground;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.3f;

    [Header("Camera")]
    [SerializeField] private Transform cameraHolder;
    [SerializeField] private float mouseSensitivity = 3f;

    [Header("Step Settings")]
    [SerializeField] private float stepHeight = 0.3f;
    [SerializeField] private float stepCheckDistance = 0.5f;

    public Transform lastCheckpoint;
    public RoomCombatManager roomCombatManager;
    private float coyoteTimer;
    private float jumpBufferTimer;

    private bool jumpPressed;
    private bool jumpReleased;
    private bool jumpHeld;

    private bool isJumping;

    private float xRotation;

    [SerializeField] private Animator anim;
    [SerializeField] private HammerHitHitbox hammerHitHitbox;

    private InputSystem_Actions inputAction;
    private Rigidbody rb;

    private void Awake()
    {
        inputAction = new InputSystem_Actions();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        inputAction.Player.Move.Enable();
        inputAction.Player.Jump.Enable();
        inputAction.Player.Look.Enable();

        inputAction.Player.Jump.performed += OnJumpPerformed;
        inputAction.Player.Jump.canceled += OnJumpCanceled;

        inputAction.Player.Attack.Enable();
        inputAction.Player.Attack.performed += OnAttackPerformed;
    }

    private void OnDisable()
    {
        inputAction.Player.Move.Disable();
        inputAction.Player.Look.Disable();

        inputAction.Player.Jump.performed -= OnJumpPerformed;
        inputAction.Player.Jump.canceled -= OnJumpCanceled;
        inputAction.Player.Jump.Disable();

        inputAction.Player.Attack.Disable();
        inputAction.Player.Attack.performed -= OnAttackPerformed;
    }

    void FixedUpdate()
    {
        Move();
        StepClimb();
        Animations();
    }

    void Update()
    {
        Look();
        // Debug.Log($"IsGrounded() { IsGrounded() }|checkSphere {groundCheck.position}|radius {groundDistance}");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (IsGrounded())
        {
            Gizmos.color = Color.green;
        }

        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        jumpPressed = true;
        jumpHeld = true;
    }

    private void OnJumpCanceled(InputAction.CallbackContext context)
    {
        jumpReleased = true;
        jumpHeld = false;
    }

    private bool IsGrounded()
    {
        

        return Physics.CheckSphere(groundCheck.position, groundDistance, ground);
    }

    private void Move()
    {
        if (jumpPressed)
        {
            jumpBufferTimer = jumpBuffer;
            jumpPressed = false;
        }
        else
        {
            jumpBufferTimer -= Time.deltaTime;
        }
        

        if (IsGrounded())
        {
            coyoteTimer = coyoteTime;
            isJumping = false;
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
        }
        
        float yVelocity = rb.linearVelocity.y;

        if (jumpBufferTimer > 0f && coyoteTimer > 0f && !isJumping)
        {
            yVelocity = jumpForce;
            isJumping = true;
            jumpBufferTimer = 0f;
            coyoteTimer = 0f;
        }

        if (jumpReleased && yVelocity > 0f)
        {
            yVelocity *= jumpCutMltp;
            jumpReleased = false;
        }

        if (yVelocity < 0f)
        {
            yVelocity += Physics.gravity.y * (fallMltp - 1) * Time.fixedDeltaTime;
        }
        else if (yVelocity > 0f && !jumpHeld)
        {
            yVelocity += Physics.gravity.y * (lowJumpMltp - 1) * Time.fixedDeltaTime;
        }

        if (yVelocity < maxFallSpeed)
        {
            yVelocity = maxFallSpeed;
        }

        Vector2 move = inputAction.Player.Move.ReadValue<Vector2>();

        Vector3 moveDirection = transform.right * move.x + transform.forward * move.y;
        moveDirection.Normalize();

        Vector3 targetVelocity = moveDirection * topSpeed;

        rb.linearVelocity = new Vector3(
            Mathf.MoveTowards(rb.linearVelocity.x, targetVelocity.x, acceleration * Time.fixedDeltaTime),
            yVelocity,
            Mathf.MoveTowards(rb.linearVelocity.z, targetVelocity.z, acceleration * Time.fixedDeltaTime)
        );
    }

    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        anim.SetTrigger("IsSwinging");
    }

    private void Look()
    {
        Vector2 look = inputAction.Player.Look.ReadValue<Vector2>();

        float mouseX = look.x * mouseSensitivity;
        float mouseY = look.y * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -40f, 10f);

        cameraHolder.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }

    public void EnableHammerHitBox()
    { 
        hammerHitHitbox.hitting = true;
    }

    public void DisableHammerHitBox()
    {
        hammerHitHitbox.hitting = false;
    }

    // Step climbing so small rocks don't block player
    private void StepClimb()
    {
        RaycastHit lowerHit;

        if (Physics.Raycast(transform.position + Vector3.up * 0.05f, transform.forward, out lowerHit, stepCheckDistance))
        {
            RaycastHit upperHit;

            if (!Physics.Raycast(transform.position + Vector3.up * stepHeight, transform.forward, out upperHit, stepCheckDistance))
            {
                rb.position += Vector3.up * stepHeight;
            }
        }
    }

    public void KillPlayer()
    {
        transform.position = lastCheckpoint.position;
        if(roomCombatManager != null)
        {
            roomCombatManager.ResetRoom();
        }

    }

    private void Animations()
    {
        //Idle and Run
        if (IsGrounded())
        {
            anim.SetBool("IsJumping", false);
            anim.SetBool("IsMidAir", false);
            bool isMoving = rb.linearVelocity.x != 0 || rb.linearVelocity.z != 0;
            anim.SetBool("IsRunning", isMoving);

            //if (anim.GetBool("IsMidAir"))
            //{
            //    anim.SetBool("IsMidAir", false);
            //}
            //if (rb.linearVelocity.x != 0 || rb.linearVelocity.z != 0)
            //{
            //    anim.SetBool("IsRunning", true);
            //}
            //else if (rb.linearVelocity.x == 0 || rb.linearVelocity.z == 0)
            //{
            //    anim.SetBool("IsRunning", false);
            //}
            if (jumpPressed)
            {
                anim.SetBool("IsJumping", true);
            }
        }
        //Mid Air
        else  
        {
            if (!anim.GetBool("IsMidAir"))
            {
                anim.SetBool("IsMidAir", true);
            }
        }
    }
}