using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Author: Allie Sangalli
 * This class handles the gui elements on the title screen
 * This class is used by the SceneManager only
 */
public class TitleManager : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// creates gui elements for the title and two buttons for playing the game and closing the application respectively
    /// </summary>
    private void OnGUI()
    {
        GUI.skin.box.fontSize = 120;
        GUI.Box(new Rect(Screen.width/2 - 450, 120, 900, 180), "Asteroid Attack");

        GUI.skin.button.fontSize = 50;

        //gui buttons for playing the game and quitting the game respectively
        if(GUI.Button(new Rect(Screen.width/2 - 100, 320, 200, 100), "Play"))
        {
            SceneManager.LoadScene("Asteroids", LoadSceneMode.Single);
        }
        if (GUI.Button(new Rect(Screen.width / 2 - 100, 440, 200, 100), "Quit"))
        {
            Application.Quit();
        }
    }
}
