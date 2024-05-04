using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    // Parallax effect needs Camera and Subject to track
    // Make Camera & Subject PUBLIC so we can specify what it will be tracking with which camera in Unity

    // Specify camera
    public Camera cam;

    // Player tracking
    // Transform = Component of all game objects that allows us to access position/rotation/scale
    public Transform subject;

    // Starting position for parallax game object
    // We only care about X and Y which is why its Vector 2 (we will need to do some casting b/w Vector 3 and 2)
    Vector2 startPosition;

    // Starting Z value of parallax game object (i.e. how far it is in the background)
    // Z value must be stored separately since it will NOT be a in the startPosition 
    float startZ;

    // Determines how much the camera has moved from the sprite since the beginning of each update's start position
    // => is LAMBDA i.e. basically allows us to specify that we want camMoveSinceStart to be the result of 
    // the camera's position - startPosition so we can get the difference
    // We MUST cast the position of the camera to Vector2 since it will otherwise give us a Vector3 (default)
    Vector2 travel => (Vector2)cam.transform.position - startPosition;

    // Distance of background object's Z position from the subject's Z position 
    // Necessary since it will help us determine the parallax factor!
    // Larger distance = background will move SLOWER
    float distanceFromSubject => transform.position.z - subject.position.z;

    // If bg object is further back, return the farClipPane otherwise return nearClipPlane & add to Z position of camera to adjust new clipping plane
    // Calculate the distance of a clipping plane from the camera based on the camera's position and whether the distance from the subject is positive or negative
    // Clipping plane determines what is visible to the camera and what gets culled (not rendered) based on whether objects are within or outside
    float clippingPlane => (cam.transform.position.z + (distanceFromSubject > 0 ? cam.farClipPlane : cam.nearClipPlane));

    // Parallax factor is the absolute value of the background's distance from the subject / clipping plane
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
