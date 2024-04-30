using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwiggyMovement : MonoBehaviour
{
    private Rigidbody2D body;

    // SerializeField allows us to access variable from unity
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float verticalSpeed;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // GetAxis("Horizontal") --> checks for L vs. R keys
        // body.velocity takes current velocity of body (i.e. set to 0)
        body.velocity = new Vector2(Input.GetAxis("Horizontal")*horizontalSpeed,  body.velocity.y);

        if(Input.GetKey(KeyCode.Space)){
            body.velocity = new Vector2(body.velocity.x, verticalSpeed);
        }


    }
}
