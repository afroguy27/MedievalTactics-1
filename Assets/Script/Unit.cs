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
	public int movesCount=0;

	public bool isEnemy;
	public int x;
	public int y;
	public bool isFocused;
	public bool hasBetrayed;
	public bool isMoved = false;
	//public bool tileSelected = false;

	[SerializeField]
	Map map;

	public void OnClick(){
		//if there is no focused unit then it makes the clicked unit the focused unit
		if (map.FocusedUnit == null) {
			print ("selecting unit");
			map.FocusedUnit = this;
			//if the focused unit has not been moved then...
			if (!map.FocusedUnit.isMoved) {
				map.highlightMovable (map.FocusedUnit.move + map.GetTile (x, y).cost, map.FocusedUnit.x, map.FocusedUnit.y);
				map.highlightAttackable (map.FocusedUnit.range, map.FocusedUnit.x, map.FocusedUnit.y);
			} else { //if the unit has been moved, deselects it
				print("deselecting");
				map.FocusedUnit = null;
			}
		} else if(map.FocusedUnit == this){
			print("deselecting");
			map.FocusedUnit = null;
			map.clearHighlightMove ();
			map.clearHighlightAttack ();
		} else {

			if (map.GetUnit (map.FocusedUnit.x, map.FocusedUnit.y).isEnemy != this.isEnemy && map.GetTile(this.x, this.y).IsAttackable) {
				print (map.FocusedUnit.name + " attacking to " + this.name);
				print (this.health + "current");
				map.AttackTo (map.GetUnit (map.FocusedUnit.x, map.FocusedUnit.y), this);
				print (this.health);
				map.clearHighlightAttack ();
				map.clearHighlightMove ();
				map.FocusedUnit = null;
			} else {
				print ("cant attack your team");
			}
		}

		/*
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
		print ("HEREEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE");
		if((destination.X==(focused.x+1)||destination.X==(focused.x-1))&&(destination.Y==(focused.y+1)||destination.Y==(focused.y-1))){
			focused.x = destination.X;
			focused.y = destination.Y;
		}

	}

	public virtual void isDead(){

	}

	public virtual void loseHealth(int ATK){

	}

	// Use this for initialization
	public void hovered(){
		if (map.FocusedUnit == null) {
			map.setHealthStr (health.ToString ());
			map.setAtkStr (ATK.ToString ());
			map.setRangeStr (range.ToString ());
		}
	}
	public void unhovered(){
		if (map.FocusedUnit == null) {
			map.setHealthStr (" ");
			map.setAtkStr (" ");
			map.setRangeStr (" ");
		}
	}
	public void updateMovesCount(){
		movesCount++;
		if (movesCount == move) {
			isMoved = true;
			movesCount = 0;
		}
	}

	public int getMovesCount(){
		return movesCount;
	}
}
