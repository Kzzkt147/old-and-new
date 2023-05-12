using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float friction;
    // private movement variables
    private Vector2 _velocity;

    [Header("Jump Settings")] 
    [SerializeField] private float jumpStrength;
    [SerializeField] private float fallMultiplier;
    [SerializeField] private float cancelJumpMultiplier;
    [SerializeField] private float coyoteTime = 0.1f;
    [SerializeField] private float jumpBuffer = 0.15f;
    [SerializeField] private float maxFallingSpeed = 10f;
    // private jump variables
    private float _coyoteTimer;
    private float _jumpBufferTimer;
    private bool _bufferActive = false;
    
    [Header("Grounded Settings")]
    [SerializeField] private Vector2 groundCheckRaycastOffset;
    [SerializeField] private float groundCheckRaycastDistance;
    [SerializeField] private LayerMask groundLayers;
    // private grounded variables
    private bool _isGrounded = true;

    [Header("Connected Components")] 
    [SerializeField] private new Rigidbody2D rigidbody;
    [SerializeField] private Animator animator;
    
    // ANIMATOR HASH
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int IsGrounded = Animator.StringToHash("IsGrounded");

    public float MoveInput { get; set; }
    public bool IsJumpPressed { get; set; }

    public void Jump()
    {
        if (!_isGrounded) StartJumpBuffer();
        if (!_isGrounded && Time.time > _coyoteTimer) return;
        
        rigidbody.AddForce(new Vector2(0f, jumpStrength), ForceMode2D.Impulse);
    }

    private void CoyoteTimerStart()
    {
        _coyoteTimer = Time.time + coyoteTime;
    }

    private void StartJumpBuffer()
    {
        _jumpBufferTimer = Time.time + jumpBuffer;
        _bufferActive = true;
    }

    private void HandleJumpMultipliers()
    {
        if (rigidbody.velocity.y < 0)
        {
            rigidbody.velocity += Vector2.up * (Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime);
        }
        else if (rigidbody.velocity.y > 0 && !IsJumpPressed)
        {
            rigidbody.velocity += Vector2.up * (Physics2D.gravity.y * (cancelJumpMultiplier - 1) * Time.fixedDeltaTime);
        }
    }

    private void UpdateGroundedStatus()
    {
        if (Physics2D.Raycast((Vector2)transform.position + new Vector2(groundCheckRaycastOffset.x, groundCheckRaycastOffset.y), Vector2.down, groundCheckRaycastDistance, groundLayers) && rigidbody.velocity.y < 0.05f)
        {
            _isGrounded = true;
        }
        else if (Physics2D.Raycast((Vector2)transform.position + new Vector2(-groundCheckRaycastOffset.x, groundCheckRaycastOffset.y), Vector2.down, groundCheckRaycastDistance, groundLayers) && rigidbody.velocity.y < 0.05f)
        {
            _isGrounded = true;
        }
        else
        {
            if(_isGrounded)
            {
                CoyoteTimerStart();
            }
            _isGrounded = false;
        }
        
        if(animator) animator.SetBool(IsGrounded, _isGrounded);
    }

    private void HorizontalMovement()
    {
        var isMoving = MoveInput != 0;
        
        if (isMoving)
        {
            _velocity = Vector2.MoveTowards(_velocity, new Vector2(MoveInput * maxSpeed, rigidbody.velocity.y), acceleration * Time.deltaTime);
            
            // Flip Character
            if (MoveInput > 0)
            {
                transform.localScale = new Vector2(1, transform.localScale.y);
            }
            else if (MoveInput < 0)
            {
                transform.localScale = new Vector2(-1, transform.localScale.y);
            }
            
        }
        else
        {
            // Apply Friction
            _velocity = Vector2.MoveTowards(_velocity, Vector2.zero, friction * Time.deltaTime);
        }
        
        animator.SetBool(IsMoving, isMoving);
    }

    private void Update()
    {
        HorizontalMovement();
        
        UpdateGroundedStatus();

        if (_bufferActive && _isGrounded && Time.time < _jumpBufferTimer)
        {
            _bufferActive = false;
            Jump();
        }

        // Clamp fall velocity
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, Mathf.Clamp(rigidbody.velocity.y, -maxFallingSpeed, Mathf.Infinity));
    }

    private void FixedUpdate()
    {
        HandleJumpMultipliers();
        
        var newVelocity = new Vector2(_velocity.x, rigidbody.velocity.y);
        rigidbody.velocity = newVelocity;
    }

    private void OnDrawGizmosSelected()
    {
        // Grounded Gizmos
        Gizmos.color = Color.blue;
        Gizmos.DrawRay((Vector2)transform.position + new Vector2(-groundCheckRaycastOffset.x, groundCheckRaycastOffset.y), Vector2.down * groundCheckRaycastDistance);
        Gizmos.DrawRay((Vector2)transform.position + new Vector2(groundCheckRaycastOffset.x, groundCheckRaycastOffset.y), Vector2.down * groundCheckRaycastDistance);
    }
}
