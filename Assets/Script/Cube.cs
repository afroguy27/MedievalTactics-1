using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {

	int health = 7;
	int ATK = 3;
	bool attacking = false;
	Cube unit;
	Cube target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Attack(){
		print("current " + target.gameObject.name + " " + health);

		//animator.SetTrigger("Attack");
		target.loseHealth(ATK);
		print(target.gameObject.name + " " + health);
		isDead ();

	}

	virtual public void OnMouseUpAsButton(){

		if (attacking == true) {
			print (this.gameObject + " attacking!");
			target = this;
			unit.Attack ();
			attacking = false;

		} else {
			print ("chosen" + this.gameObject.name);
			attacking = true;
			unit = this;
		}
	}

	void loseHealth(int ATK){
		health -= ATK;
	}

	void isDead(){
		if(health <= 0 ){
			print ("Destroying " + this.gameObject.name);
			Destroy (this.gameObject);
		}
	}

}
