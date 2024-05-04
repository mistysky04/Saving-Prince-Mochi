using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class TwiggyMovement : MonoBehaviour
{

    Vector2 moveInput;
    Rigidbody2D rigidBody;

    // initial value of isMoving
    private bool _isMoving = false;

    Animator animator;
    public bool IsMoving
    {
        get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            animator.SetBool("IsMoving", value);
        }
    }
    public float walkSpeed = 5f;

    // Finds component as soon as game starts up (even before Start)
    // Ensures rigidbody exists on component
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate() 
    {
        // y controlled by gravity, NOT player controls
        rigidBody.velocity = new Vector2(moveInput.x * walkSpeed, rigidBody.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context) 
    {
        // X & Y movement input
        moveInput = context.ReadValue<Vector2>();

        // IsMoving is true when moveInput is NOT zero
        IsMoving = moveInput != Vector2.zero;
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsMoving = true;
        } else if (context.canceled)
        {
            IsMoving = false;
        }
    }
}
