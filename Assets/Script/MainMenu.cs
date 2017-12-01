using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	public Scene scene;
	public Button button;

	// Use this for initialization
	void Start () {
		Button start = button.GetComponent<Button> ();
		start.onClick.AddListener (OnClick);
	}

	//when the button is clicked start the game(the story)
	public void OnClick() {
		//StartGame.Invoke ();
		//scene = SceneManager.GetSceneByBuildIndex (1);
		SceneManager.LoadScene (1); //loads the MiniMap1 scene
	}
}
