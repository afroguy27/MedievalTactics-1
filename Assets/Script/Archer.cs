using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Archer : Unit {


	// Update is called once per frame
	void Update () {
		/*if (this.isEnemy) {
			sprite = this.GetComponent<Image> ();
			this.sprite.sprite = redTeam;
		} else {
			sprite = this.GetComponent<Image> ();
			this.sprite.sprite = blueTeam;
		}*/
	}

	public override void isDead(){
		if(health <= 0 ){
			print ("Destroying " + this.gameObject.name);
			if (!this.isEnemy) {
				Destroy (this.gameObject);
			} else {
				Destroy (this.gameObject);
			}
		}
	}

	public override void loseHealth(int ATK){
		health -= ATK;
	}


	/*public override void betray(){
		var rand = UnityEngine.Random.Range(0, 10);
		if (!hasBetrayed && rand == 0) {
			//change sprite image
			//remove this unit from the Unit list it is currently in 
			//add to the opponent side unit list 

		}
	}
	*/
}
