using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class TwiggyMovement : MonoBehaviour
{
    private Rigidbody2D body;

    Vector2 moveInput;

    public bool IsMoving { get; private set; }

    // SerializeField allows us to access variable from unity
    public float walkSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate() 
    {

    }

    void OnMove(InputAction.CallbackContext context) 
    {
        // X & Y movement input
        // Ensure its not 0, otherwise isMoving will be set to FALSE
        moveInput = context.ReadValue<Vector2>();

        IsMoving = moveInput != Vector2.zero;
    }
}
