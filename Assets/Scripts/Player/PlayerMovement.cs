using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;

    public float groundDrag;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump = true; // посмотреть

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;

    [Header("KeyBinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Ground_Checked")]
    public float playersHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementState state;

    bool canMove = true;

    public enum MovementState 
    {
        walking,
        sprinting,
        crouching,
        air
    }

    private void OnEnable()
    {
        FlashlightChip.SetPlayerMovementDisabled += ChangeMoveStatus;
    }

    private void OnDisable()
    {
        FlashlightChip.SetPlayerMovementDisabled -= ChangeMoveStatus;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        startYScale = transform.localScale.y;
    }

    void Custom_Input() 
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //Прыжок
        if (Input.GetKey(jumpKey) && readyToJump && grounded) 
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(Reset_Jump), jumpCooldown);
        }

        //Ползежка
        if (Input.GetKeyDown(crouchKey)) 
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        if (Input.GetKeyUp(crouchKey)) 
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }

    private void StateHandler() 
    {
        //Ползем
        if (Input.GetKey(crouchKey))  // Поменать на зажатие и потом сделать отжатие как волк спиид
        {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
        }

        //Бег
        if (grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }
        else if (grounded) // Ходьба
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }
        else 
        {
            state = MovementState.air;
        }

    }

    void Player_Move() 
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //Под углом
        if (OnSlope() && !exitingSlope) 
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);

            // После отключения гравитации -> Мы не прыгаем!
            if (rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

        // отключаем гравитацию когда под наклоном
        rb.useGravity = !OnSlope();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove) 
        {
            grounded = Physics.Raycast(transform.position, Vector3.down, playersHeight * 0.5f + 0.1f, whatIsGround); // проверка на нахождение на земле

            Custom_Input();
            SpeedControl();
            StateHandler();

            if (grounded)
                rb.drag = groundDrag;
            else
                rb.drag = 0;
        }
    }

    private void FixedUpdate()
    {

        Player_Move();
    }

    private void SpeedControl() 
    {
        //Ограничиваем скорость под наклоном
        if (OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }
        else // Ограничиваем скорость на земле и воздухе 
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed; //считаем должную максимальную скорость
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z); //применяем
            }
        } 
    }

    private void Jump() 
    {
        exitingSlope = true;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void Reset_Jump() 
    {
        readyToJump = true;
        exitingSlope = false;
    }

    private bool OnSlope() 
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playersHeight * 0.5f + 0.3f)) 
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }
        return false;
    }

    private Vector3 GetSlopeMoveDirection() 
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }

    void ChangeMoveStatus() 
    {
        canMove = false;
    }

}
