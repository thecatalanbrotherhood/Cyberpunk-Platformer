using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Rigidbody2D characterRb;

    [SerializeField] float speed = 5f;
    //[SerializeField] float runningSpeed = 10f;
    private float moveInput;
    [SerializeField] float movementSmoothing = .05f;
    private Vector3 refVelocity = Vector3.zero;
    private bool facingRight = true;

    private bool isGrounded;
    [SerializeField] Transform groundCheck;
    [SerializeField] float checkRadius = 0.75f;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float longFallMulti = 2.5f;
    [SerializeField] int maxJumps = 2;
    [SerializeField] int extraJumps;
    private float jumpTime;
    private float groundTime;
    [SerializeField] float rememberJumpTime = 0.2f;
    [SerializeField] float rememberGroundTime = 0.2f;

    //Health values
    public int maxHealth = 5;
    public int currentHealth;
    public HealthBar healthBar;

    public bool isActive = true;

    private void Start()
    {
        if (healthBar != null)
        {
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
        }
    }

    private void Awake()
    {
        characterRb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
    }

    private void Update()
    {
        if (isActive)
        {
            Jump();
            GroundMovement();
            if (Input.GetKeyDown("k"))
            {
                TakeDamage(1);
            }

            if (isGrounded && Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Grounded");
            }
        }
    }

    private void GroundMovement()
    {
        moveInput = Input.GetAxis("Horizontal");
        Vector3 targetVelocity = new Vector2(moveInput * speed, characterRb.velocity.y);
        characterRb.velocity = Vector3.SmoothDamp(characterRb.velocity, targetVelocity, ref refVelocity, movementSmoothing);
        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    private void Jump()
    {
        jumpTime -= Time.deltaTime;
        groundTime -= Time.deltaTime;

        if (isGrounded == true)
        {
            extraJumps = maxJumps;
            groundTime = rememberGroundTime;
        }

        if (Input.GetKeyDown("space") || Input.GetKeyDown("w"))
        {
            jumpTime = rememberJumpTime;

            if (isGrounded == false)
            {
                if (extraJumps > 0 && jumpTime > 0)
                {
                    jumpTime = 0;
                    characterRb.velocity = Vector2.up * jumpForce;
                    extraJumps--;
                }
            }
        }

        if (isGrounded == true || groundTime > 0)
        {
            if (extraJumps > 0 && jumpTime > 0)
            {
                jumpTime = 0;
                characterRb.velocity = Vector2.up * jumpForce;
            }
        }
        characterRb.velocity += Vector2.up * Physics2D.gravity.y * (longFallMulti - 1) * Time.deltaTime;
    }
}
