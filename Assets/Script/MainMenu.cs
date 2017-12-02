using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour {

	//when the button is clicked start the game(the story)
	public void OnClick() {
		SceneManager.LoadScene (0); //loads the MiniMap1 scene
	}

	public void ReturnToMainMenu() {
		SceneManager.LoadScene (1); //Loads the Scene0 scene
	}
}
