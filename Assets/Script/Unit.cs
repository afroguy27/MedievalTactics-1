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
	public int vision;
	public int rations;
	public Sprite blueTeam;
	public Sprite redTeam;

	public bool isEnemy;
	public int x;
	public int y;
	public bool isFocused;
	public bool hasBetrayed;
	public bool isMoved = false;

	public Image sprite;

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
			map.FocusedUnit = null; //deselects the unit
			map.clearHighlightMove ();
			map.clearHighlightAttack ();
		} else {

			if (map.GetUnit (map.FocusedUnit.x, map.FocusedUnit.y).isEnemy != this.isEnemy && map.GetTile(this.x, this.y).IsAttackable) {
				print (map.FocusedUnit.name + " attacking to " + this.name);
				print (this.health + "current");
				map.AttackTo (map.GetUnit (map.FocusedUnit.x, map.FocusedUnit.y), this);
				print (this.health);
				map.GetUnit (map.FocusedUnit.x, map.FocusedUnit.y).discolor ();
				map.clearHighlightAttack ();
				map.clearHighlightMove ();
				map.FocusedUnit = null;
			} else {
				print ("cant attack your team");
			}
		}
	}


	/*public void moveUnit(Tile destination, Unit focused){
		print ("HEREEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE");
		if((destination.X==(focused.x+1)||destination.X==(focused.x-1))&&(destination.Y==(focused.y+1)||destination.Y==(focused.y-1))){
			focused.x = destination.X;
			focused.y = destination.Y;
		}
	}*/

	public virtual void isDead(){

	}

	public virtual void loseHealth(int ATK){

	}
		
	public void discolor(){
		var image = GetComponent<Image> ();
		image.color = new Color (0.4f, 0.4f, 0.4f);
	}

	public void recolor(){
		var image = GetComponent<Image> ();
		image.color = new Color (1.0f, 1.0f, 1.0f);
	}
		
	/*public void betray(){
		for (int i = 0; i < map.unitsAlly.Count; i++) {
			if (map.unitsAlly [i].rations <= 2) {
				sprite = map.unitsAlly [i].GetComponent<Image> ();

				if(map.unitsAlly[i].CompareTag("BlueArcher")) {
					map.unitsAlly [i].sprite.sprite = redTeam;
				}
				else if(map.unitsAlly[i].CompareTag("RedArcher")) {
					map.unitsAlly [i].sprite.sprite = blueTeam;
				}
				else if(map.unitsAlly[i].CompareTag("BlueSwordsman")) {
					map.unitsAlly [i].sprite.sprite = redTeam;
				}
				else if(map.unitsAlly[i].CompareTag("RedSwordsman")) {
					map.unitsAlly [i].sprite.sprite = blueTeam;
				}

				map.unitsAlly [i].isEnemy = true;
				map.unitsAlly [i].hasBetrayed = true;
				map.unitsEnemy.Add (map.unitsAlly [i]);
				map.unitsAlly.RemoveAt(i);

			}
		}
	}*/

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
