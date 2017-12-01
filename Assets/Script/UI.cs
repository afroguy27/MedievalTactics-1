using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

	public int healthToDisplay=0;
	public Unit selected;
	public Map map;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (map.getFocused () != null) {
			selected = map.getFocused ();
			// TODO: Broke Unity Please Fix
			//healthToDisplay = selected.getHealth ();
		} else {
			healthToDisplay = 0;
		}
		//selected = map.getFocused ();


		Text gt = this.GetComponent<Text> ();
		gt.text = "Health of selected unit: " + healthToDisplay;
	}
}
