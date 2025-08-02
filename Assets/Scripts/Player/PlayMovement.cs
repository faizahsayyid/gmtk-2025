using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float coyoteTime = 0.2f; // Time allowed to jump after leaving the ground
    public float jumpBufferTime = 0.1f; // Time allowed to jump before landing

    [Header("Air Control Settings")]
    public float acceleration = 20f;
    public float airAcceleration = 10f;
    public float maxHorizontalSpeed = 8f;
    public float horizontalDrag = 0.95f;


    [Header("Ground Check Settings")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Vector2 groundCheckBoxSize = new Vector2(0.5f, 0.1f);

    [Header("Animation Settings")]
    public Animator animator;

    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.5f;
    public float castDelay = 0.2f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private float coyoteTimeCounter;
    private float jumpBufferCounter;
    private float nextFireTime;

    private PlayerInputActions inputActions;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputActions = new PlayerInputActions();

        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        inputActions.Player.Jump.performed += ctx => jumpBufferCounter = jumpBufferTime;
        inputActions.Player.Attack.performed += ctx => Shoot();
    }

    private void OnEnable() => inputActions.Enable();
    private void OnDisable() => inputActions.Disable();

    // Update is called once per frame
    void Update()
    {
        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime; // Reset coyote time when grounded
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime; // Decrease coyote time counter
        }

        // Jump buffer update
        if (jumpBufferCounter > 0)
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        // Perform jump if both timers valid
        if (jumpBufferCounter > 0 && coyoteTimeCounter > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            animator.SetInteger(PlayerAnimationConstants.Accessor, PlayerAnimationConstants.Jump);          
            jumpBufferCounter = 0f;
            coyoteTimeCounter = 0f;
        }
    }

    // FixedUpdate is used for physics calculations
    private void FixedUpdate()
    {
        float targetSpeed = moveInput.x * maxHorizontalSpeed;

        float currentSpeed = rb.linearVelocity.x;
        float speedDiff = targetSpeed - currentSpeed;

        float accelRate = IsGrounded() ? acceleration : airAcceleration;
        float movement = speedDiff * accelRate;

        rb.AddForce(new Vector2(movement, 0f));

        // Add walk animation
        if (moveInput.x != 0)
        {
            animator.SetInteger(PlayerAnimationConstants.Accessor, PlayerAnimationConstants.Walk);
            // Flip the player based on movement direction
            if (moveInput.x > 0)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (moveInput.x < 0)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }
        else
        {
            animator.SetInteger(PlayerAnimationConstants.Accessor, PlayerAnimationConstants.Idle);
        }

        // Apply horizontal drag when airborne and no input
        if (!IsGrounded() && Mathf.Approximately(moveInput.x, 0f))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x * horizontalDrag, rb.linearVelocity.y);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapBox(groundCheck.position, groundCheckBoxSize, 0f, groundLayer);
    }

    private void Shoot()
    {
        Debug.Log("1)" + (Time.time >= nextFireTime));
        Debug.Log("2)" + (bulletPrefab != null));
        Debug.Log("3)" + (firePoint != null));
        if (Time.time >= nextFireTime && bulletPrefab != null && firePoint != null)
        {
            Debug.Log("Shooting");
            nextFireTime = Time.time + fireRate;

            // animator.SetInteger(PlayerAnimationConstants.Accessor, PlayerAnimationConstants.Cast);

            StartCoroutine(FireBulletAfterDelay());
        }
    }

    private IEnumerator FireBulletAfterDelay()
    {
        Debug.Log("Firing bullet after delay");
        yield return new WaitForSeconds(castDelay);

        SpawnManager.InstantiateAndNotify(
            bulletPrefab,
            firePoint.position,
            firePoint.rotation,
            transform.localScale.x <= 0
        );
    }
}
