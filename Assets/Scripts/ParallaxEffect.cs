using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{

    public Camera cam;

    // Player tracking
    public Transform subject;

    // Starting position for parallax game object
    Vector2 startPosition;

    // Starting Z value of parallax game object (i.e. how far it is in the background)
    float startZ;


    Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startPosition;

    // => means value after is calculated during each update
    // Distance camera has moved from original sprite position 
    Vector2 travel => (Vector2)cam.transform.position - startPosition;
    float distanceFromSubject => transform.position.z - subject.position.z;

    float clippingPlane => (cam.transform.position.z + (distanceFromSubject > 0 ? cam.farClipPlane : cam.nearClipPlane));
    float parallaxFactor => Mathf.Abs(distanceFromSubject) / clippingPlane;

    // Start is called before the first frame update
    void Start()
    {
        // startPosition is Vector2 BUT transform.position provides Vector3 
        // Chops off Z value so must store this in separate variable
        startPosition = transform.position;
        startZ = transform.position.z;
        
    }

    // Update is called once per frame
    void Update()
    {
        // When the target moves, move the parallax object the same distance times a multiplier
        Vector2 newPosition = startPosition + travel * parallaxFactor;

        // The X/Y position changes based on target travel speed * parallax factor BUT Z stays constant
        transform.position = new Vector3(newPosition.x, newPosition.y, startZ);
        
    }
}
