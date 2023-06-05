using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] GameObject attackRotate;

    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;

    public float groundDrag;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    public float smooth = 1f;
    private Quaternion targetRotation;

    Rigidbody rb;

    public MovementState state;

    public enum MovementState
    {
        walking,
        air
    }

    bool running = true;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        targetRotation = transform.rotation;
        readyToJump = true;
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight, whatIsGround);

        MyInput();
        StateHandler();

        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.down * playerHeight, Color.blue);
            rb.drag = 0;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            targetRotation *= Quaternion.AngleAxis(90, Vector3.up);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            targetRotation *= Quaternion.AngleAxis(-90, Vector3.up);
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

    }

    private void StateHandler()
    {

        // walking
        if (grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }

        // air
        else
        {
            state = MovementState.air;
        }

    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 2f * airMultiplier, ForceMode.Force);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 10 * smooth * Time.deltaTime);

        if (horizontalInput != 0 || verticalInput != 0)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection);
            if (!EnemySensor.CurrentTargetObject)
                attackRotate.transform.rotation = Quaternion.RotateTowards(attackRotate.transform.rotation, toRotation, 1000 * Time.deltaTime);
        }
    }

    private void Jump()
    {

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

}
