using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	public GameObject button, R_level, level_1, level_2, howto;

	public void Story () {
		gameObject.SetActive(false);
		howto.SetActive(false);
		R_level.SetActive (false);
		level_1.SetActive (true);
		level_2.SetActive (true);
	}
	//when the button is clicked start the game(the story)
	public void Level_1() {
		SceneManager.LoadScene (0); //loads the MiniMap1 scene
	}

	public void Level_2() {
		SceneManager.LoadScene (4);
	}

	public void Level_R() {
		SceneManager.LoadScene (5);
	}

	public void ReturnToMainMenu() {
		SceneManager.LoadScene (1); //Loads the Scene0 scene
	}
		
}
