using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 8;
    public float SprintMultiplier = 2;
    public float GroundDrag = 6;

    public float JumpForce;
    public float jumpCooldown;
    public float airDrag;
    bool CanJump = true;

    [Header("Keybinds")]
    public KeyCode JumpKey = KeyCode.Space;
    public KeyCode SprintKey = KeyCode.LeftShift;

    bool Grounded;
    bool isSprinting;


    [Header("Other")]
    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    float baseMoveSpeed;
    float baseDrag;

    Vector3 moveDirection;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        baseMoveSpeed = moveSpeed;
        baseDrag = rb.drag;
    }

    private void Update()
    {
        PlayerInput();
        SpeedLimit();

        if (Grounded)
        {
            rb.drag = GroundDrag;
        }

        Vector3 flatVel1 = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //NormalJump
        if (Input.GetKey(JumpKey) && Grounded && CanJump)
        {
            CanJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }



        if (Input.GetKeyDown(SprintKey) && Grounded)
        {
            Sprint();
        }

        if (Input.GetKeyUp(SprintKey))
        {
            moveSpeed = baseMoveSpeed;
        }
    }

    private void Movement()
    {
        //calculates move direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (Grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airDrag, ForceMode.Force);
        }
    }

    private void SpeedLimit() //Could also use clamping but this provides more consistent results
    {
        Vector3 flatVel2 = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel2.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel2.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); //resets the y velocity before jumping so that you always jump the same height
        rb.AddForce(transform.up * JumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        CanJump = true;
    }

    private void Sprint()
    {
        moveSpeed = moveSpeed * SprintMultiplier;
    }


    //Jump Collision Detection
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Grounded = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Grounded = false;
        }
    }
}
