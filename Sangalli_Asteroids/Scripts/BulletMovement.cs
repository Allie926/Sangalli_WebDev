using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author: Allie Sangalli
 * This class handles all movement for bullets fired by the ship
 * This class is used only by objects instantiated from the bullet prefab
 */
public class BulletMovement : MonoBehaviour {

    //velocity
    public Vector3 direction;
    private Vector3 velocity;

    //camera
    public Camera cam;
    private float camWidth;
    private float camHeight;

    // Use this for initialization
    void Start () {
        transform.Rotate(0, 0, 180); //rotates the bullet so it faces the direction it's fired
        velocity = .7f * direction; //sets the velocity to a scalar multiple of the direction vector

        camHeight = 2f * cam.orthographicSize;
        camWidth = camHeight * cam.aspect;
	}
	
	// Update is called once per frame
	void Update () {
        //move the bullet based on its velocity
        transform.position += velocity;

        //destroys the bullet if it moves beyond any edge of the screen
        if(GetComponent<SpriteRenderer>().bounds.min.x > cam.transform.position.x + camWidth / 2)
        {
            Destroy(gameObject);
        }
        else if (GetComponent<SpriteRenderer>().bounds.max.x < cam.transform.position.x - camWidth / 2)
        {
            Destroy(gameObject);
        }
        if (GetComponent<SpriteRenderer>().bounds.min.y > cam.transform.position.y + camHeight / 2)
        {
            Destroy(gameObject);
        }
        else if (GetComponent<SpriteRenderer>().bounds.max.y < cam.transform.position.y - camHeight / 2)
        {
            Destroy(gameObject);
        }
    }
}
