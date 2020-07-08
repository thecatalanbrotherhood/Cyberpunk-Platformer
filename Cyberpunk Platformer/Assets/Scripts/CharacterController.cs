using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]float movementSmoothing = .05f;

    private Rigidbody2D characterRb;

    private float speed = 5f;
    private float moveInput;

    private bool isGrounded;
    [SerializeField] Transform groundCheck;
    [SerializeField] float checkRadius = 0.75f;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] int extraJumps = 2;

    private void Awake()
    {
        characterRb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        moveInput = Input.GetAxis("Horizontal");
        characterRb.velocity = new Vector2(moveInput * speed, characterRb.velocity.y);
    }

    private void Update()
    {


        if (Input.GetKeyDown("space") || Input.GetKeyDown("w"))
        {
            if (isGrounded == true)
            {
                extraJumps = 2;
            }

            if (extraJumps > 0)
            {
                characterRb.velocity = Vector2.up * jumpForce;
                extraJumps--;
            }
        }
    }
}
