using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Unit {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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

	public override void betray(){

	}
}
