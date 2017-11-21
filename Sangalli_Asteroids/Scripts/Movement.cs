using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author: Allie Sangalli
 * This class handles all movement for the ship, including acceleration, deceleration, and wrapping around the edges of the screen
 * This class is used only by single ship object in the game
 */
public class Movement : MonoBehaviour {

    //objects
    public Camera cam;
    public GameObject ship;

    //for velocity and position
    public Vector3 direction;
    public Vector3 position;
    public Vector3 velocity;
    public float speed;

    //for acceleration
    public Vector3 acceleration;
    public float accelRate;
    public float maxSpeed;
    public float decelRate;

    //for rotation
    public float totalRotation;
    public float anglePerFrame;

    //for camera
    private float camWidth;
    private float camHeight;

    //for ship
    private float shipWidth;
    private float shipHeight;

	// Use this for initialization
	void Start () {
        //resets the ship's various fields to the defaults when the script starts
        gameObject.transform.position = new Vector3(0, 0, 0);
        direction = new Vector3(0, 1, 0);
        velocity = Vector3.zero;
        acceleration = Vector3.zero;
        totalRotation = 0;

        position = transform.position;

        camHeight = 2f * cam.orthographicSize;
        camWidth = camHeight * cam.aspect;

        shipHeight = ship.GetComponent<SpriteRenderer>().bounds.max.y - transform.position.y;
        shipWidth = ship.GetComponent<SpriteRenderer>().bounds.max.x - transform.position.x;
    }
	
	// Update is called once per frame
	void Update () {
        //checks if the left or right arrow keys are being pressed to rotate the ship
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            totalRotation += anglePerFrame;
            direction = Quaternion.Euler(0, 0, anglePerFrame) * direction;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            totalRotation -= anglePerFrame;
            direction = Quaternion.Euler(0, 0, -anglePerFrame) * direction;
        }

        //accelerates the ship if the up arrow is pressed, decelerates the ship if it's not
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Accelerate();
        }
        else
        {
            Decelerate();
        }

        //set the position and rotation of the ship
        transform.position = position;
        WrapPosition();
        transform.rotation = Quaternion.Euler(0, 0, totalRotation);
	}

    /// <summary>
    /// increases the speed of the ship based on an acceleration rate until it reaches a max speed
    /// </summary>
    private void Accelerate()
    {
        acceleration = accelRate * direction;
        velocity += acceleration;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        position += velocity;
    }

    /// <summary>
    /// decreases the speed of the ship based on a decelerations rate until it comes to a stop
    /// </summary>
    private void Decelerate()
    {
        velocity = decelRate * velocity;
        position += velocity;
    }

    /// <summary>
    /// wraps the ship's position around the edges of the screen
    /// </summary>
    private void WrapPosition()
    {
        //if the ship moves beyond the right side
        if(GetComponent<SpriteRenderer>().bounds.min.y > cam.transform.position.y + camHeight / 2)
        {
            position = new Vector3(position.x, cam.transform.position.y - (camHeight / 2) - shipHeight, position.z);
        }
        //if the ship moves beyond the left side
        else if (GetComponent<SpriteRenderer>().bounds.max.y < cam.transform.position.y - camHeight / 2)
        {
            position = new Vector3(position.x, cam.transform.position.y + (camHeight / 2) + shipHeight, position.z);
        }
        //if the ship moves beyond the top
        if (GetComponent<SpriteRenderer>().bounds.min.x > cam.transform.position.x + camWidth / 2)
        {
            position = new Vector3(cam.transform.position.x - (camWidth / 2) - shipWidth, position.y, position.z);
        }
        //if the ship moves beyond the bottom
        else if (GetComponent<SpriteRenderer>().bounds.max.x < cam.transform.position.x - camWidth / 2)
        {
            position = new Vector3(cam.transform.position.x + (camWidth / 2) + shipWidth, position.y, position.z);
        }
    }
}
