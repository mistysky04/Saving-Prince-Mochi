using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class TwiggyMovement : MonoBehaviour
{

    Vector2 moveInput;
    Rigidbody2D rigidBody;

    public float walkSpeed = 5f;

    // initial value of isMoving
    private bool _isMoving = false;

    private bool _isFacingRight = true;



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
            animator.SetBool(AnimationStrings.IsMoving, value);
        }
    }

    public bool FacingRight {
        get
        {
            return _isFacingRight;
        }
        private set
        {
            // If player isn't already facing the same direction, multiply it's movement by -1 (flip x scale)
            if(_isFacingRight != value)
            {
                // set facing direction as value
                transform.localScale *= new Vector2(-1, 1);
                _isFacingRight = value;
            }
        }
    }

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
        // Controls position of rigidbody
        rigidBody.velocity = new Vector2(moveInput.x * walkSpeed, rigidBody.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context) 
    {
        // X & Y movement input
        moveInput = context.ReadValue<Vector2>();

        // IsMoving is true when moveInput is NOT zero
        IsMoving = moveInput != Vector2.zero;

        setFacingDirection(moveInput);
    }

    private void setFacingDirection(Vector2 moveInput)
    {
        // check if player is trying to move R & isn't already facing R
        // MUST check if isn't already facing that direction, otherwise vector will be improperly flipped
        if(moveInput.x >  0 && !FacingRight)
        {
            FacingRight = true;

        // check if player is trying to move L & isn't already facing L
        } else if (moveInput.x < 0 && FacingRight)
        {
            FacingRight = false;
        }
    }

    //public void OnRun(InputAction.CallbackContext context)
    //{
    //    moveInput = context.ReadValue<Vector2>();

    //    if (context.started)
    //    {
    //        IsMoving = true;

    //    } else if (context.canceled)
    //    {
    //        IsMoving = false;
    //    }
    //}
}
