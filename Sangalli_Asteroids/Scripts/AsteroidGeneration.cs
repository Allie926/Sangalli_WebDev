using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author: Allie Sangalli
 * This class handles the generation of asteroid as the game runs
 * This class is used by the SceneManager object
 */
public class AsteroidGeneration : MonoBehaviour {

    //various fields
    private int cooldown;
    private Vector3 direction;

    //asteroid prefabs
    public GameObject asteroid1;
    public GameObject asteroid2;
    public GameObject asteroid3;

    //camera fields
    public Camera cam;
    private float camHeight;
    private float camWidth;

	// Use this for initialization
	void Start () {
        cooldown = 0;
        camHeight = 2f * cam.orthographicSize;
        camWidth = camHeight * cam.aspect;
	}
	
	// Update is called once per frame
	void Update () {
        //generate an asteroid after every few seconds
		if(cooldown <= 0)
        {
            GameObject generated = null;
            //randomly chooses one of three prefabs for the generated asteroid to add variety to the sprites
            int rng = Random.Range(0, 3);
            if(rng == 0)
            {
                generated = Instantiate(asteroid1, RandomPosition(), Quaternion.identity);
            }
            else if(rng == 1)
            {
                generated = Instantiate(asteroid2, RandomPosition(), Quaternion.identity);
            }
            else if(rng == 2)
            {
                generated = Instantiate(asteroid3, RandomPosition(), Quaternion.identity);
            }

            //set the direction vector of the generated asteroid
            generated.GetComponent<AsteroidMovement>().direction = direction.normalized;

            //cooldown for generating new asteroids
            //as the score increases beyond certain thresholds, the cooldown decreases, causing the game to get harder over time
            int score = gameObject.GetComponent<CollisionDetection>().score;
            cooldown = 90 - (score/300)*15;
        }

        //count down the cooldown every frame
        cooldown--;
	}

    /// <summary>
    /// generates a random position for the asteroid to be generated at
    /// </summary>
    /// <returns>1 of 8 possible starting locations</returns>
    private Vector3 RandomPosition()
    {
        //sets a position in each corner and side of the screen
        Vector3[] positions = new Vector3[8];
        positions[0] = new Vector3(cam.transform.position.x + camWidth / 2, 0, 0);
        positions[1] = new Vector3(cam.transform.position.x + camWidth / 2, cam.transform.position.y + camHeight / 2, 0);
        positions[2] = new Vector3(0, cam.transform.position.y + camHeight / 2, 0);
        positions[3] = new Vector3(cam.transform.position.x - camWidth / 2, cam.transform.position.y + camHeight / 2, 0);
        positions[4] = new Vector3(cam.transform.position.x - camWidth / 2, 0, 0);
        positions[5] = new Vector3(cam.transform.position.x - camWidth / 2, cam.transform.position.y - camHeight / 2, 0);
        positions[6] = new Vector3(0, cam.transform.position.y - camHeight / 2, 0);
        positions[7] = new Vector3(cam.transform.position.x + camWidth / 2, cam.transform.position.y - camHeight / 2, 0);

        //randomly sets the direction based on the position of the asteroid
        //generates direction based on the starting location so no asteroid is generated and immediately travels off screen
        int rng = Random.Range(0, 8);
        switch (rng)
        {
            case 0:
                direction = new Vector3(Random.Range(-1, -0.1f), Random.Range(-1, 1), 0);
                break;
            case 1:
                direction = new Vector3(Random.Range(-1, -0.1f), Random.Range(-1, -0.1f), 0);
                break;
            case 2:
                direction = new Vector3(Random.Range(-1, 1), Random.Range(-1, -0.1f), 0);
                break;
            case 3:
                direction = new Vector3(Random.Range(0.1f, 1), Random.Range(-1, -0.1f), 0);
                break;
            case 4:
                direction = new Vector3(Random.Range(0.1f, 1), Random.Range(-1, 1), 0);
                break;
            case 5:
                direction = new Vector3(Random.Range(0.1f, 1), Random.Range(0.1f, 1), 0);
                break;
            case 6:
                direction = new Vector3(Random.Range(-1, 1), Random.Range(0.1f, 1), 0);
                break;
            case 7:
                direction = new Vector3(Random.Range(-1, -0.1f), Random.Range(0.1f, 1), 0);
                break;
        }
        return positions[rng];
    }
}
