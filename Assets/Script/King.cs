using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using DG.Tweening;

public class King : Unit {

	//public King(int hp, int at, int r, int move) : base(hp, at, r, move){
	//	isEnemy = false;
	//

	//int health = 5;
	//int ATK = 2;
	//int range = 1;
	//int move = 2;

	//[SerializeField]
	//Map map;

	public override void isDead(){
		if(health <= 0 ){
			print ("Destroying " + this.gameObject.name);
			if (!this.isEnemy) {
				Destroy (this.gameObject);
				SceneManager.LoadScene (2);
			} else {
				Destroy (this.gameObject);
				SceneManager.LoadScene (3);
			}
		}
	}

	public override void loseHealth(int ATK){
		health -= ATK;
	}
		
	/*public override void betray(){
		//Kings do not betray
		//this function should be empty
	}*/

	//void OnMouseOver(){
	//	print (gameObject.name);
	//	print("Health: " + health);
	//	print ("ATK: " + ATK);
	//	print ("Range: " + range);
	//}

}
