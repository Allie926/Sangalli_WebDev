using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Author: Allie Sangalli
 * This class handles all collision detection for every object in the game, as well as keeps track of the number of lives the player has and their score
 * Collisions handled include ship with any level asteroid, bullet with level1 asteroid, and bullet with level2 asteroid
 * This class is used on the SceneManager object 
 */
public class CollisionDetection : MonoBehaviour {

    //lists of all asteroids and bullets
    private GameObject[] asteroids;
    private GameObject[] asteroids2;
    private GameObject[] bullets;

    //ship fields
    public GameObject ship;
    private int lives;
    public int score;

    //asteroid prefabs
    public GameObject asteroid1;
    public GameObject asteroid2;
    public GameObject asteroid3;

	// Use this for initialization
	void Start () {
        lives = 3;
        score = 0;

        asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        asteroids2 = GameObject.FindGameObjectsWithTag("AsteroidLv2");
        bullets = GameObject.FindGameObjectsWithTag("Bullet");

        for(int i = 0; i < asteroids.Length;)
        {
            Destroy(asteroids[i]);
        }
        for (int i = 0; i < asteroids2.Length;)
        {
            Destroy(asteroids2[i]);
        }
        for (int i = 0; i < bullets.Length;)
        {
            Destroy(bullets[i]);
        }
    }
	
	// Update is called once per frame
	void Update () {
        //sets the asteroid and bullet lists to every instance of those types of objects active in the scene
        //each game object is tagged, level 1 asteroids with Asteroid, level 2 asteroids with AsteroidLv2, and bullets with Bullet
        //makes it so I can just get a list of every GameObject with that tag in the scene
        asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        asteroids2 = GameObject.FindGameObjectsWithTag("AsteroidLv2");
        bullets = GameObject.FindGameObjectsWithTag("Bullet");

        //checks collision between the ship and level 1 asteroids
        for (int i = 0; i < asteroids.Length; i++)
        {
            //if a collision occurs, destroy the asteroid and subtract a life
            if (CheckCollision(ship, asteroids[i]))
            {
                Destroy(asteroids[i]);
                lives--;
            }
        }

        //checks collision between the ship and level 2 asteroids
        for (int i = 0; i < asteroids2.Length; i++)
        {
            //if a collision occurs, destroy the asteroid and subtract a life
            if (CheckCollision(ship, asteroids2[i]))
            {
                Destroy(asteroids2[i]);
                lives--;
            }
        }

        //checks collision between each bullet and each level 1 asteroid
        for (int i = 0; i < bullets.Length; i++)
        {
            for(int j = 0; j < asteroids.Length; j++)
            {
                //if a collision occurs, generate two level 2 asteroids, destroy the original asteroid and the bullet, and increase the score
                if (CheckCollision(bullets[i], asteroids[j]))
                {
                    GenerateAsteroid(asteroids[j]);
                    GenerateAsteroid(asteroids[j]);

                    Destroy(asteroids[j]);
                    Destroy(bullets[i]);
                    score += 20;
                }
            }
        }

        //check collision between each bullet and each level 2 asteroid
        for (int i = 0; i < bullets.Length; i++)
        {
            for (int j = 0; j < asteroids2.Length; j++)
            {
                //if a collision occurs, destroy both the asteroid and the bullet and increase the score
                if (CheckCollision(bullets[i], asteroids2[j]))
                {
                    Destroy(asteroids2[j]);
                    Destroy(bullets[i]);
                    score += 50;
                }
            }
        }

        //if the player runs out of lives, destroy the ship
        if (lives <= 0)
        {
            Destroy(ship);
        }
	}

    /// <summary>
    /// checks collisions between two GameObjects using Bounding Circles
    /// </summary>
    /// <param name="obj1">the first object</param>
    /// <param name="obj2">the second object</param>
    /// <returns>whether or not a collision occurred</returns>
    private bool CheckCollision(GameObject obj1, GameObject obj2)
    {
        //center and radius of object 1
        Vector3 center1 = obj1.GetComponent<SpriteRenderer>().bounds.center;
        float radius1 = obj1.GetComponent<SpriteRenderer>().bounds.max.x - center1.x;

        //center and radius of object 2
        Vector3 center2 = obj2.GetComponent<SpriteRenderer>().bounds.center;
        float radius2 = obj2.GetComponent<SpriteRenderer>().bounds.max.x - center2.x;

        //if the distance between the two objects' centers is less than the sum of their radii, a collision occurred
        if(Vector3.Distance(center1,center2) < radius1 + radius2)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// generates GUI for the score and lives, as well as when the player gets a game over
    /// </summary>
    private void OnGUI()
    {
        //gui containing the score and the remaining lives
        GUI.skin.box.fontSize = 30;
        GUI.Box(new Rect(5, 5, 300, 50), score + "\tLives: " + lives);

        //if the player loses, generate a gui that says Game Over
        if(lives <= 0)
        {
            GUI.skin.box.fontSize = 120;
            GUI.Box(new Rect(Screen.width/2 - 325, 200, 650, 180), "Game Over");
            if (GUI.Button(new Rect(Screen.width/2 - 200, 400, 400, 150), "Back to Menu"))
            {
                SceneManager.LoadScene("Title", LoadSceneMode.Single);
            }
        }
    }

    /// <summary>
    /// generates an asteroid based on the parent object
    /// </summary>
    /// <param name="parent">the parent asteroid</param>
    /// <returns>the generated asteroid object</returns>
    private GameObject GenerateAsteroid(GameObject parent)
    {
        GameObject generated = null;
        //randomly generates the asteroid from 1 of 3 different prefabs, allowing for variation in asteroid sprites
        int random = Random.Range(0, 3);
        if(random == 0)
        {
            generated = Instantiate(asteroid1, parent.transform.position, parent.transform.rotation);
        }
        else if(random == 1)
        {
            generated = Instantiate(asteroid2, parent.transform.position, parent.transform.rotation);
        }
        else if(random == 2)
        {
            generated = Instantiate(asteroid3, parent.transform.position, parent.transform.rotation);
        }

        //set the direction of the generated asteroid based on the direction of the parent asteroid with slight variation
        Vector3 direction = parent.GetComponent<AsteroidMovement>().direction;
        generated.GetComponent<AsteroidMovement>().direction = new Vector3(direction.x + Random.Range(-.1f, .1f), direction.y + Random.Range(-.1f, .1f), 0);
        return generated;
    }
}
