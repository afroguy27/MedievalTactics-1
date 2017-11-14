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
	//public bool isFocused;
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

		if (!map.unitisAttacking) {
			map.FocusedUnit = this;
			this.isMoved = true;
			map.unitisAttacking = true;
			print (map.FocusedUnit.gameObject.name);
			print("x: " + map.FocusedUnit.x + " y: " + map.FocusedUnit.y);
			print(map.GetTile(map.FocusedUnit.x,  map.FocusedUnit.y).X + " " +map.GetTile(map.FocusedUnit.x,  map.FocusedUnit.y).Y);
			map.highlightAttackable(map.FocusedUnit.range, map.FocusedUnit.x, map.FocusedUnit.y);
		} else {
			if (map.FocusedUnit == this) {
				print ("cant attack yourself");
				return;
			}else{
				print (this.health);
				print (map.FocusedUnit.gameObject.name+ " attacking to " + this.gameObject.name);
				map.AttackTo (map.FocusedUnit, this);
				//this.loseHealth (this.ATK);
				print (this.health);
				this.isDead ();
				map.FocusedUnit = null;
				map.unitisAttacking = false;
				map.eraseHighlight();
			}
		}
	}


	public virtual void isDead(){

	}

	public virtual void loseHealth(int ATK){

	}

	public int getHealth(){
		return health;
	}
}
