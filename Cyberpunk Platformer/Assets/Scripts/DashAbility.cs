using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : MonoBehaviour
{
    private Rigidbody2D rb;
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int direction;

    [SerializeField] bool airDash;

    private bool isGrounded;
    [SerializeField] Transform groundCheck;
    [SerializeField] float checkRadius = 0.75f;
    [SerializeField] LayerMask whatIsGround;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
    }

    void Update()
    {
        Dash();
    }

    private void Dash()
    {
        if (airDash || isGrounded)
        {
            if (direction == 0)
            {
                if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && Input.GetKeyDown(KeyCode.LeftShift))
                {
                    Debug.Log("Left");
                    direction = 1;
                }
                if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && Input.GetKeyDown(KeyCode.LeftShift))
                {
                    direction = 2;
                    Debug.Log("Right");
                }
            }
            else
            {
                if (dashTime <= 0)
                {
                    direction = 0;
                    dashTime = startDashTime;
                    rb.velocity = Vector2.zero;
                }
                else
                {
                    dashTime -= Time.deltaTime;

                    if (direction == 1)
                    {
                        rb.velocity = Vector2.left * dashSpeed;
                    }
                    else if (direction == 2)
                    {
                        rb.velocity = Vector2.right * dashSpeed;
                    }
                }
            }
        }
    }
}
