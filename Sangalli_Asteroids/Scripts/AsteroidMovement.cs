using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author: Allie Sangalli
 * This class handles all movement for the various asteroids in the game
 * This class is used by every object instantiated from any one of the six asteroid prefabs
 */
public class AsteroidMovement : MonoBehaviour {

    //direction and velocity
    public Vector3 direction;
    private Vector3 velocity;

	// Use this for initialization
	void Start () {
        velocity = direction * Random.Range(0.05f, 0.2f); //generates the asteroid's velocity with slight variation in the speed
	}
	
	// Update is called once per frame
	void Update () {
        //moves the asteroid based on velocity
        transform.position += velocity;

        //destroys an asteroid if it travels beyond the edges of the camera
        //had to hard code the exact locations since a camera in a scene cannot be placed in a field in a prefab
        Bounds bounds = gameObject.GetComponent<SpriteRenderer>().bounds;
        if(bounds.min.y > 25) //goes beyond the top
        {
            Destroy(gameObject);
        }
        else if(bounds.max.y < -25) //goes beyong the bottom
        {
            Destroy(gameObject);
        }
        if(bounds.min.x > 30) //goes beyond the right
        {
            Destroy(gameObject);
        }
        else if(bounds.max.x < -30) //goes beyond the left
        {
            Destroy(gameObject);
        }
	}
}
