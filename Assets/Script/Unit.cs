using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using DG.Tweening;

public class Unit : MonoBehaviour {

	public int health;
	public int ATK;
	public int range;
	public int move;

	public bool isEnemy;
	public int x;
	public int y;
	public bool isFocused;
	public bool hasBetrayed;
	public bool isMoved;

	[SerializeField]
	Map map;

	//public Unit(int hp, int at, int r, int move){
	//	health = hp;
	//	ATK = at;
	//	range = r;
	//	movement = move;
	//}

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void OnClick(){
		//if (map.GetTile (x, y).IsAttackable) {

		if (!this.isMoved) {
			map.FocusedUnit = this;
			this.isMoved = true;
		} else {
			print (this.health);
			print (this.gameObject + " attacking to " + this.gameObject);
			map.AttackTo (map.FocusedUnit, this);
			//this.loseHealth (this.ATK);
			print (this.health);
			this.isDead ();
			map.FocusedUnit = null;
		}
		//}
	}


	public virtual void isDead(){

	}

	public virtual void loseHealth(int ATK){

	}

}
