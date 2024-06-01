using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirection : MonoBehaviour
{

    // ContactFilter2D: allows us to specify what we want to filter the ray to interact with in our game
    public ContactFilter2D castFilter;

    // distance we want the ray to travel i.e. how close do we want to be to the ground for us to be considered "grounded"?
    public float groundDistance = 0.05f;
    public float wallDistance = 0.2f;
    public float ceilingDistance = 0.05f;

    CapsuleCollider2D touchingCol;

    // Array to store return value of our raycast
    // Raycast will return ALL collisions that occur, we will store these in an array
    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];

    Animator animator;

    private bool _isGrounded = true;
    private bool _isOnWall = false;
    private bool _isOnCeiling = false;

    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    public bool IsGrounded
    {
        get
        {
            return _isGrounded;
        }
        private set
        {
            _isGrounded = value;
            animator.SetBool(AnimationStrings.IsGrounded, value);
        }
    }

    public bool IsOnWall
    {
        get
        {
            return _isOnWall;
        }
        private set
        {
            _isOnWall = value;
            animator.SetBool(AnimationStrings.IsOnWall, value);
        }
    }

    public bool IsOnCeiling
    {
        get
        {
            return _isOnCeiling;
        }
        private set
        {
            _isOnCeiling = value;
            animator.SetBool(AnimationStrings.IsOnCeiling, value);
        }
    }

    private void Awake()
    {
        // Save character collider to variable (in this case, MUST be capsuleCollider)
        touchingCol = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        // Collider2D has a Cast fn which allows us to cast a ray from it while describing the direction, filters, results, and distance of the ray
        // by DEFAULT, .Cast returns 1 if the ray is interacting with something, and 0 if it is not i.e. 1 if character is grounded and 0 if not
        IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        IsOnWall = touchingCol.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0;
        IsOnCeiling = touchingCol.Cast(Vector2.up, castFilter, ceilingHits, ceilingDistance) > 0;

        // How we checked output of touchingCol.Cast
        //Debug.Log(touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance));
    } 
}
