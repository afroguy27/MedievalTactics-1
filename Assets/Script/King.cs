﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
				print ("GameOver");
			} else {
				Destroy (this.gameObject);
				print ("You won");
			}
		}
	}

	public override void loseHealth(int ATK){
		health -= ATK;
	}

	void OnMouseOver(){
		print (gameObject.name);
		print("Health: " + health);
		print ("ATK: " + ATK);
		print ("Range: " + range);
	}

}