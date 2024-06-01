using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Forced to have BOTH components
[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection))]

public class Angie : MonoBehaviour
{

    public float walkSpeed = 3f;

    Rigidbody2D rb;

    public enum WalkableDirection { Right, Left };

    private WalkableDirection _walkDirection;

    private Vector2 walkDirectionVector;

    TouchingDirection touchingDirection;

    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set
        {
            if (_walkDirection != value)
            {
                //Direction Flipped
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if (value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;

                } else if (value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirection = GetComponent<TouchingDirection>(); 

    }

    private void FixedUpdate()
    {
        if(touchingDirection.IsGrounded && touchingDirection.IsOnWall)
        {
            FlipDirection();
        }
        rb.velocity = new Vector2(walkSpeed + walkDirectionVector.x, rb.velocity.y);
    }

    private void FlipDirection()
    {
        if(WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        } else if(WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        } else
        {
            Debug.LogError("Current walkable direction is not set to legal values Right or Left");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
