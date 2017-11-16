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
	public bool isMoved = false;
	//public bool tileSelected = false;

	[SerializeField]
	Map map;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {

	}

	void OnClick(){
		/*
		if (map.GetTile(x, y).IsAttackable)
		{
			map.AttackTo(map.FocusedUnit, this);
			return;
		}

		if (null != map.FocusedUnit && this != map.FocusedUnit)
		{
			map.FocusedUnit.isFocused = false;
			map.clearHighlightAttack();
			map.clearHighlightMove ();
		}

		isFocused = !isFocused;
		if (isFocused)
		{
			map.highlightMovable(move, x, y);
			map.highlightAttackable(range, x, y);
		}
		else
		{
			map.clearHighlightAttack();
			map.clearHighlightMove ();

		}
		*/
		if (map.FocusedUnit == null) {
			print("selecting unit");
			map.FocusedUnit = this;
			//highlight moveable tile;
			map.highlightMovable(this.move, this.x, this.y);
		} else {
			if (this == map.FocusedUnit) {
				print ("Deselecting from here");
				map.clearHighlightMove ();
				map.FocusedUnit = null;
			}
			else if (!map.unitisAttacking) {

				print("not attacking unit");
				print (this.x + " " + this.y);
				moveUnit (map.destinationTile, this);
				isMoved = true;
				map.unitisAttacking = true;
				map.highlightAttackable (map.FocusedUnit.range, map.FocusedUnit.x, map.FocusedUnit.y);
			} else {
				if (map.FocusedUnit == this) {
					print("deselect");
					map.FocusedUnit = null;
					map.clearHighlightAttack();
					map.unitisAttacking = false;
				} else {
					print (this.health);
					//print (map.FocusedUnit.gameObject.name+ " attacking to " + this.gameObject.name);
					map.AttackTo (map.FocusedUnit, this);
					print (this.health);
					this.isDead ();
					map.FocusedUnit = null;
					map.unitisAttacking = false;
					map.clearHighlightAttack();
				}
			}
		}
		/*	
		if (!map.unitisAttacking) {
			map.FocusedUnit = this;
			this.isMoved = true;
			map.unitisAttacking = true;
			//print (map.FocusedUnit.gameObject.name);
			//print("x: " + map.FocusedUnit.x + " y: " + map.FocusedUnit.y);
			//print(map.GetTile(map.FocusedUnit.x,  map.FocusedUnit.y).X + " " +map.GetTile(map.FocusedUnit.x,  map.FocusedUnit.y).Y);
			map.highlightAttackable(map.FocusedUnit.range, map.FocusedUnit.x, map.FocusedUnit.y);
		} else {
			if (map.FocusedUnit == this) {
				//print ("cant attack yourself");
				return;
			}else{
				print (this.health);
				//print (map.FocusedUnit.gameObject.name+ " attacking to " + this.gameObject.name);
				map.AttackTo (map.FocusedUnit, this);
				//this.loseHealth (this.ATK);
				print (this.health);
				this.isDead ();
				map.FocusedUnit = null;
				map.unitisAttacking = false;
				map.eraseHighlight();
			}
		}*/
	}
		

	public void moveUnit(Tile destination, Unit focused){
		focused.x = destination.X;
		focused.y = destination.Y;

	}

	public virtual void isDead(){

	}

	public virtual void loseHealth(int ATK){

	}

	public int getHealth(){
		return health;
	}
}
