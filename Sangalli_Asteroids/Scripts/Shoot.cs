using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author: Allie Sangalli
 * This class handles the ship's ability to fire bullets
 * This class is used only by the single ship object in the game
 */
public class Shoot : MonoBehaviour {

    //bullet prefab and various fields
    public GameObject bullet;
    private GameObject shot;
    private int cooldown;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        //fires a bullet in the same direction but faster speed as the ship if the space key is pressed and the cooldown is complete
		if(Input.GetKeyDown(KeyCode.Space) && cooldown <= 0)
        {
            Vector3 startPos = GetComponent<SpriteRenderer>().bounds.max;
            shot = Instantiate(bullet, transform.position, transform.rotation);
            shot.GetComponent<BulletMovement>().direction = GetComponent<Movement>().direction;
            shot.GetComponent<BulletMovement>().cam = GetComponent<Movement>().cam;
            cooldown = 20;
        }
        cooldown--;
	}
}
