﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using DG.Tweening;

public class Map : MonoBehaviour {

	[SerializeField]
	Tile groundPrefab;
	[SerializeField]
	Tile waterPrefab;
	[SerializeField]
	Tile bridgePrefab;
	[SerializeField]
	Tile rockPrefab;
	[SerializeField]
	Tile treePrefab;

	[SerializeField]
	Transform UnitContainer;

	GameObject touchBlocker;

	public Unit FocusedUnit;
	public bool unitisAttacking;
	public Tile destinationTile;

	List<Tile> tiles = new List<Tile>();
	List<Tile> highlightedMoveTiles = new List<Tile>();
	List<Tile> highlightedAttackTiles = new List<Tile>();

	public Button button; //button instance
	public bool playerTurn = true; //boolean value used for determining player turn

	//Stuff for UI
	string healthStr=" ";
	string atkStr=" ";
	string rangeStr=" ";
	public Text healthtxt;
	public Text atktxt;
	public Text rangetxt;


	//lists of ally and enemy units
	public List<Unit> unitsAlly = new List<Unit> (10);
	public List<Unit> unitsEnemy = new List<Unit> (10);

	//public Unit FocusedUnit
	//{
	//	get { return UnitContainer.GetComponentsInChildren<Unit>().FirstOrDefault(x => x.isFocused);}
	//}

	public void PutUnit(int x, int y, Unit Prefab, bool isEnemy){
		//print ("putting unit " + Prefab.name);
		var unit = Instantiate (Prefab);
		unit.isEnemy = isEnemy;
		unit.gameObject.SetActive (true);
		unit.transform.SetParent (UnitContainer);
		unit.transform.position = tiles.First (c => c.X == x && c.Y == y).transform.position;
		unit.x = x;
		unit.y = y;

		//if the current unit being placed is an enemy adds it to the enemy list
		if (isEnemy) {
			unitsEnemy.Add (unit);
		} else {
			//adds to the ally list
			unitsAlly.Add (unit);
		}

	}

	public Tile GetTile(int x, int y)
	{
		//print ("searching for tile (" + x +", " + y + ")");
		Tile myTile;
		myTile = tiles.FirstOrDefault(c => c.X == x && c.Y == y);
		//print ("myTile: (" + myTile.X + ", " + myTile.Y + ")");
		return myTile;
	}

	public Unit GetUnit(int x, int y)
	{
		return UnitContainer.GetComponentsInChildren<Unit>().FirstOrDefault(u => u.x == x && u.y == y);
	}

	public void AttackTo(Unit fromUnit, Unit toUnit)
	{
		toUnit.loseHealth (fromUnit.ATK);
		//FocusedUnit.isMoved = true;
		toUnit.isDead();
	}

	public void highlightAttackable(int range, int x, int y){
		// print ("range is " + range);
		if (range == 0) {
			highlightAttack(x, y);
			//highlightedAttackTiles.Add (GetTile(x,y));
			//print (x + ", " + y + " is attackable.");

		} else {
			highlightAttack(x, y);
			//highlightedAttackTiles.Add (GetTile(x,y));
			//print (x + ", " + y + " is attackable.");
			if (x != 0) {
				highlightAttackable (range - 1, x - 1, y);
			}
			if (x != 19) {
				highlightAttackable (range - 1, x + 1, y);
			}
			if (y != 0) {
				highlightAttackable (range - 1, x, y - 1);
			}
			if (y != 9) {
				highlightAttackable (range - 1, x, y + 1);
			}
		}
	}

	public void highlightMovable(int movement, int x, int y){
		int cost = GetTile (x, y).cost;
		//print ("(" +x + ", " + y+ ")\t" + "movement: "+ movement + "\tcost:" + cost);
		//print ("range is " + range);
		if (movement - cost == 0) {
			highlightMove(x, y);
			highlightedMoveTiles.Add (GetTile(x,y));
			//print (x + ", " + y + " is movable.");
		}else if(movement - cost < 0){
			return;
		} else {
			highlightMove(x, y);
			highlightedMoveTiles.Add (GetTile(x,y));
			//print (x + ", " + y + " is movable.");
			if (x != 0) {
				highlightMovable (movement - cost, x - 1, y);
			}
			if (x != 19) {
				highlightMovable (movement - cost, x + 1, y);
			}
			if (y != 0) {
				highlightMovable (movement - cost, x, y - 1);
			}
			if (y != 9) {
				highlightMovable (movement - cost, x, y + 1);
			}
		}
	}

	public void highlightAttack(int x, int y){
		if (GetTile (x, y) == null) {
			return;
		} else {
			if(GetTile(x, y).Unit != null && GetTile(x, y).Unit.isEnemy != FocusedUnit.isEnemy){
				GetTile(x, y).IsAttackable = true;
				highlightedAttackTiles.Add (GetTile(x,y));
				//GetTile (x, y).Unit.GetComponent<Button> ().interactable = true;
			}
		}
	}

	public void highlightMove(int x, int y){
		if (GetTile (x, y) == null) {
			return;
		} else {
			GetTile (x, y).IsMovable = true;
		}
	}

	public void clearHighlightAttack(){
		for (int i = 0; i < highlightedAttackTiles.Count; i++) {
			highlightedAttackTiles [i].IsAttackable = false;
		}
		highlightedAttackTiles.Clear ();
	}

	public void clearHighlightMove(){
		for (int i = 0; i < highlightedMoveTiles.Count; i++) {
			//print ("debug " + highlightedMoveTiles [i].X);
			highlightedMoveTiles [i].IsMovable =false;
		}
		highlightedMoveTiles.Clear ();
	}

	public void moveUnit(Tile target, Unit Focus){
		if (!Focus.isMoved) {
			clearHighlightMove ();
			clearHighlightAttack ();
			print ("debug " + Focus.x + " " + Focus.y);
			print (GetUnit (Focus.x, Focus.y).gameObject.name);
			GetUnit (Focus.x, Focus.y).transform.position = tiles.First (c => c.X == target.X && c.Y == target.Y).transform.position;
			print ("debug " + Focus.x + " " + Focus.y);
			FocusedUnit.x = target.X;
			FocusedUnit.y = target.Y;
			highlightAttackable (FocusedUnit.range, FocusedUnit.x, FocusedUnit.y);
			if (highlightedAttackTiles.Count == 0) {
				print ("Cannot attack hence deselect");
				GetUnit (FocusedUnit.x, FocusedUnit.y).discolor ();
				FocusedUnit = null;
			}
			Focus.isMoved = true;
			Focus.rations -= 1;
		}
	}

	// Use this for initialization
	void Start () {
		foreach (var prefab in new Tile[]{groundPrefab, waterPrefab, bridgePrefab})
		{
			prefab.gameObject.SetActive(false);
		}
	}

	// Update is called once per frame
	void Update () {
		//TODO: Broke Unity Please Fix
		healthtxt.text = "Health: " + healthStr;
		atktxt.text = "Attack: " + atkStr;
		rangetxt.text = "Attack Range: " + rangeStr;
	}

	//method that switches the turn by setting the isMoved flag to true for one side
	//and doing the opposite for the other
	public void OnClick() {
		print(unitsAlly.Count+ " units in list");
		//switches the allies isMoved
		for (int i = 0; i < unitsAlly.Count; i++) {
			unitsAlly [i].recolor ();
			if(unitsAlly[i].isMoved == !playerTurn)
				unitsAlly[i].isMoved = !unitsAlly[i].isMoved;
		}

		if (playerTurn)
			betrayBlue (); //any ally units that have less than 2 rations will betray
		
		
		//switches the enemies button
		for (int i = 0; i < unitsEnemy.Count; i++) {
			unitsEnemy [i].recolor ();
			if(unitsEnemy[i].isMoved == playerTurn)
				unitsEnemy[i].isMoved = !unitsEnemy[i].isMoved;
		}

		if(!playerTurn)
			betrayRed (); //same for red side
		
		playerTurn = !playerTurn;
		print("It is " + playerTurn + " that it is the player's turn");
	}
		
	//goes through evry unit and decides if they will betray
	public void betrayBlue(){
		//moves through the units
		for (int i = 0; i < unitsAlly.Count; i++) {
			//if the unit has 2 or less rations and has not betrayed yet
			if (unitsAlly [i].rations <= 2 && !unitsAlly[i].hasBetrayed) {
				//gets the Image component of the unit
				unitsAlly[i].sprite = unitsAlly [i].GetComponent<Image> ();
				//if the unit is a blue archer, makes it a red archer
				if(unitsAlly[i].CompareTag("BlueArcher")) {
					unitsAlly [i].sprite.sprite = unitsAlly[i].redTeam; //changes the sprite
				}
				//if the unit is a blue swordman, makes it a red swordman
				else if(unitsAlly[i].CompareTag("BlueSwordsman")) {
					unitsAlly [i].sprite.sprite = unitsAlly[i].redTeam; //changes the sprite
				}

				unitsAlly [i].isEnemy = true; //sets it to enemy status
				unitsAlly [i].hasBetrayed = true; //sets that it has betrayed
				unitsEnemy.Add (unitsAlly [i]); //adds the unit to the enemy list
				unitsAlly.RemoveAt(i); //removes the unit from the ally list

			}
		}
			
	}

	//goes through evry unit and decides if they will betray
	public void betrayRed(){
		//moves through the units
		for (int i = 0; i < unitsEnemy.Count; i++) {
			//if the unit has 2 or less rations and has not betrayed yet
			if (unitsEnemy [i].rations <= 2 && !unitsEnemy[i].hasBetrayed) {
				//gets the Image component of the unit
				unitsEnemy[i].sprite = unitsEnemy [i].GetComponent<Image> ();
				//if the unit is a red archer, makes it a blue archer
				if(unitsEnemy[i].CompareTag("RedArcher")) {
					unitsEnemy [i].sprite.sprite = unitsEnemy[i].blueTeam; //changes the sprite
				}
				//if the unit is a red swordman, makes it a blue swordman
				else if(unitsEnemy[i].CompareTag("RedSwordsman")) {
					unitsEnemy [i].sprite.sprite = unitsEnemy[i].blueTeam; //changes the sprite
				}

				unitsEnemy [i].isEnemy = false; //sets it to enemy status
				unitsEnemy [i].hasBetrayed = true; //sets that it has betrayed
				unitsAlly.Add (unitsAlly [i]); //adds the unit to the enemy list
				unitsEnemy.RemoveAt(i); //removes the unit from the ally list

			}
		}

	}

	public class Coordinate{
		public readonly int x;
		public readonly int y;

		public Coordinate(int x, int y){
			this.x = x;
			this.y = y;
		}
	}

	public class CoordinateAndValue
	{
		public readonly Coordinate coordinate;
		public readonly int value;

		public CoordinateAndValue(int x, int y, int value)
		{
			this.coordinate = new Coordinate(x, y);
			this.value = value;
		}
	}

	public Unit getFocused(){
		return FocusedUnit;
	}

	public void setHealthStr(string a){
		// TODO: Broke Unity Please Fix
		healthStr = a;
	}

	public void setAtkStr(string a){
		// TODO: Broke Unity Please Fix
		atkStr = a;
	}

	public void setRangeStr(string a){
		// TODO: Broke Unity Please Fix
		rangeStr = a;
	}
		
	public void GenerateMap(){

		foreach (var tile in tiles) {
			Destroy (tile.gameObject);
		}

		//for (var i = 0; i < 5; i++) {
		//	for(var k = 0; k < 10; k++){
		//		//print ("Setting Coordinate : " + i + k);
		//		Tile tile;
		//		tile = Instantiate(groundPrefab);
		//		tile.gameObject.SetActive(true);
		//		tile.transform.SetParent(transform);
		//		tile.SetCoordinate (i, k);
		//		tiles.Add(tile);
		//	}
		//}

		//oth column
		for (var i = 0; i < 10; i++) {
			//print ("Setting Coordinate : " + i + 5);
			Tile tile;
			tile = Instantiate(groundPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (0, i);
			tiles.Add(tile);
		}

		//1st column
		for (var i = 0; i < 9; i++) {
			//print ("Setting Coordinate : " + i + 5);
			Tile tile;
			tile = Instantiate(groundPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (1, i);
			tiles.Add(tile);
		}

		for (var i = 9; i < 10; i++) {
			//print ("Setting Coordinate : " + i + 5);
			Tile tile;
			tile = Instantiate(rockPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (1, i);
			tiles.Add(tile);
		}

		//2nd column
		for (var i = 0; i < 1; i++) {
			//print ("Setting Coordinate : " + i + 5);
			Tile tile;
			tile = Instantiate(groundPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (2, i);
			tiles.Add(tile);
		}

		for (var i = 1; i < 2; i++) {
			//print ("Setting Coordinate : " + i + 5);
			Tile tile;
			tile = Instantiate(rockPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (2, i);
			tiles.Add(tile);
		}

		for (var i = 2; i < 10; i++) {
			//print ("Setting Coordinate : " + i + 5);
			Tile tile;
			tile = Instantiate(groundPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (2, i);
			tiles.Add(tile);
		}

		//3rd column
		for (var i = 0; i < 10; i++) {
			//print ("Setting Coordinate : " + i + 5);
			Tile tile;
			tile = Instantiate(groundPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (3, i);
			tiles.Add(tile);
		}

		//4th column
		for (var i = 0; i < 3; i++) {
			//print ("Setting Coordinate : " + i + 5);
			Tile tile;
			tile = Instantiate(groundPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (4, i);
			tiles.Add(tile);
		}

		for (var i = 3; i < 4; i++) {
			//print ("Setting Coordinate : " + i + 5);
			Tile tile;
			tile = Instantiate(rockPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (4, i);
			tiles.Add(tile);
		}

		for (var i = 4; i < 10; i++) {
			//print ("Setting Coordinate : " + i + 5);
			Tile tile;
			tile = Instantiate(groundPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (4, i);
			tiles.Add(tile);
		}

		//5th column
		for (var i = 0; i < 6; i++) {
			//print ("Setting Coordinate : " + i + 5);
			Tile tile;
			tile = Instantiate(groundPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (5, i);
			tiles.Add(tile);
		}

		for (var i = 6; i < 7; i++) {
			//print ("Setting Coordinate : " + i + 5);
			Tile tile;
			tile = Instantiate(treePrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (5, i);
			tiles.Add(tile);
		}

		for (var i = 7; i < 9; i++) {
			//print ("Setting Coordinate : " + i + 5);
			Tile tile;
			tile = Instantiate(bridgePrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (5, i);
			tiles.Add(tile);
		}

		for (var i = 9; i < 10; i++) {
			//print ("Setting Coordinate : " + i + 5);
			Tile tile;
			tile = Instantiate(groundPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (5, i);
			tiles.Add(tile);
		}

		//6th column
		for (var i = 0; i < 5; i++) {
			//print ("Setting Coordinate : " + i + 6);
			Tile tile;
			tile = Instantiate(groundPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (6, i);
			tiles.Add(tile);
		}

		for (var i = 5; i < 7; i++) {
			//print ("Setting Coordinate : " + i + 6);
			Tile tile;
			tile = Instantiate(waterPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (6, i);
			tiles.Add(tile);
		}

		for (var i = 7; i < 9; i++) {
			//print ("Setting Coordinate : " + i + 6);
			Tile tile;
			tile = Instantiate(bridgePrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (6, i);
			tiles.Add(tile);
		}

		for (var i = 9; i < 10; i++) {
			//print ("Setting Coordinate : " + i + 6);
			Tile tile;
			tile = Instantiate(waterPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (6, i);
			tiles.Add(tile);
		}

		//7th column
		for (var i = 0; i < 4; i++) {
			//print ("Setting Coordinate : " + i + 7);
			Tile tile;
			tile = Instantiate(groundPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (7, i);
			tiles.Add(tile);
		}

		for (var i = 4; i < 7; i++) {
			//print ("Setting Coordinate : " + i + 7);
			Tile tile;
			tile = Instantiate(waterPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (7, i);
			tiles.Add(tile);
		}


		for (var i = 7; i < 9; i++) {
			//print ("Setting Coordinate : " + i + 7);
			Tile tile;
			tile = Instantiate(bridgePrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (7, i);
			tiles.Add(tile);
		}

		for (var i = 9; i < 10; i++) {
			//print ("Setting Coordinate : " + i + 7);
			Tile tile;
			tile = Instantiate(groundPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (7, i);
			tiles.Add(tile);
		}

		//8th column
		for (var i = 0; i < 1; i++) {
			//print ("Setting Coordinate : " + i + 8);
			Tile tile;
			tile = Instantiate(groundPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (8, i);
			tiles.Add(tile);
		}

		for (var i = 1; i < 2; i++) {
			//print ("Setting Coordinate : " + i + 8);
			Tile tile;
			tile = Instantiate(bridgePrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (8, i);
			tiles.Add(tile);
		}

		for (var i = 2; i < 3; i++) {
			//print ("Setting Coordinate : " + i + 8);
			Tile tile;
			tile = Instantiate(groundPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (8, i);
			tiles.Add(tile);
		}

		for (var i = 3; i < 6; i++) {
			//print ("Setting Coordinate : " + i + 8);
			Tile tile;
			tile = Instantiate(waterPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (8, i);
			tiles.Add(tile);
		}

		for (var i = 6; i < 10; i++) {
			//print ("Setting Coordinate : " + i + 8);
			Tile tile;
			tile = Instantiate(groundPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (8, i);
			tiles.Add(tile);
		}

		//9th column
		for (var i = 0; i < 1; i++) {
			//print ("Setting Coordinate : " + i + 9);
			Tile tile;
			tile = Instantiate(waterPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (9, i);
			tiles.Add(tile);
		}

		for (var i = 1; i < 2; i++) {
			//print ("Setting Coordinate : " + i + 9);
			Tile tile;
			tile = Instantiate(bridgePrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (9, i);
			tiles.Add(tile);
		}

		for (var i = 2; i < 5; i++) {
			//print ("Setting Coordinate : " + i + 9);
			Tile tile;
			tile = Instantiate(waterPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (9, i);
			tiles.Add(tile);
		}

		for (var i = 5; i < 8; i++) {
			//print ("Setting Coordinate : " + i + 9);
			Tile tile;
			tile = Instantiate(groundPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (9, i);
			tiles.Add(tile);
		}

		for (var i = 8; i < 9; i++) {
			//print ("Setting Coordinate : " + i + 9);
			Tile tile;
			tile = Instantiate(treePrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (9, i);
			tiles.Add(tile);
		}

		for (var i = 9; i < 10; i++) {
			//print ("Setting Coordinate : " + i + 9);
			Tile tile;
			tile = Instantiate(groundPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (9, i);
			tiles.Add(tile);
		}


		//10th column
		for (var i = 0; i < 1; i++) {
			//print ("Setting Coordinate : " + i + 10);
			Tile tile;
			tile = Instantiate(groundPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (10, i);
			tiles.Add(tile);
		}

		for (var i = 1; i < 2; i++) {
			//print ("Setting Coordinate : " + i + 10);
			Tile tile;
			tile = Instantiate(bridgePrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (10, i);
			tiles.Add(tile);
		}

		for (var i = 2; i < 6; i++) {
			//print ("Setting Coordinate : " + i + 10);
			Tile tile;
			tile = Instantiate(groundPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (10, i);
			tiles.Add(tile);
		}

		for (var i = 6; i < 7; i++) {
			//print ("Setting Coordinate : " + i + 10);
			Tile tile;
			tile = Instantiate(treePrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (10, i);
			tiles.Add(tile);
		}

		for (var i = 7; i < 10; i++) {
			//print ("Setting Coordinate : " + i + 10);
			Tile tile;
			tile = Instantiate(groundPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (10, i);
			tiles.Add(tile);
		}


		//11th column
		for (var i = 0; i < 4; i++) {
			//print ("Setting Coordinate : " + i + 10);
			Tile tile;
			tile = Instantiate(groundPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (11, i);
			tiles.Add(tile);
		}

		for (var i = 4; i < 5; i++) {
			//print ("Setting Coordinate : " + i + 10);
			Tile tile;
			tile = Instantiate(treePrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (11, i);
			tiles.Add(tile);
		}

		for (var i = 5; i < 10; i++) {
			//print ("Setting Coordinate : " + i + 10);
			Tile tile;
			tile = Instantiate(groundPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (11, i);
			tiles.Add(tile);
		}

		//12th column
		for (var i = 0; i < 10; i++) {
			//print ("Setting Coordinate : " + i + 10);
			Tile tile;
			tile = Instantiate(groundPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (12, i);
			tiles.Add(tile);
		}

		//13th column
		for(var i = 0; i < 9; i++){
			//print ("Setting Coordinate : " + i + k);
			Tile tile;
			tile = Instantiate(groundPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (13, i);
			tiles.Add(tile);
		}

		for(var i = 9; i < 10; i++){
			//print ("Setting Coordinate : " + i + k);
			Tile tile;
			tile = Instantiate(waterPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (13, i);
			tiles.Add(tile);
		}

		//14th column
		for(var i = 0; i < 2; i++){
			//print ("Setting Coordinate : " + i + k);
			Tile tile;
			tile = Instantiate(groundPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (14, i);
			tiles.Add(tile);
		}

		for(var i = 2; i < 3; i++){
			//print ("Setting Coordinate : " + i + k);
			Tile tile;
			tile = Instantiate(rockPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (14, i);
			tiles.Add(tile);
		}

		for(var i = 3; i < 8; i++){
			//print ("Setting Coordinate : " + i + k);
			Tile tile;
			tile = Instantiate(groundPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (14, i);
			tiles.Add(tile);
		}

		for(var i = 8; i < 10; i++){
			//print ("Setting Coordinate : " + i + k);
			Tile tile;
			tile = Instantiate(waterPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (14, i);
			tiles.Add(tile);
		}

		//15th
		for(var i = 0; i < 7; i++){
			//print ("Setting Coordinate : " + i + k);
			Tile tile;
			tile = Instantiate(groundPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (15, i);
			tiles.Add(tile);
		}

		for(var i = 7; i < 10; i++){
			//print ("Setting Coordinate : " + i + k);
			Tile tile;
			tile = Instantiate(waterPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (15, i);
			tiles.Add(tile);
		}

		//16th
		for(var i = 0; i < 7; i++){
			//print ("Setting Coordinate : " + i + k);
			Tile tile;
			tile = Instantiate(groundPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (16, i);
			tiles.Add(tile);
		}

		for(var i = 7; i < 10; i++){
			//print ("Setting Coordinate : " + i + k);
			Tile tile;
			tile = Instantiate(waterPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (16, i);
			tiles.Add(tile);
		}

		//17th
		for(var i = 0; i < 7; i++){
			//print ("Setting Coordinate : " + i + k);
			Tile tile;
			tile = Instantiate(groundPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (17, i);
			tiles.Add(tile);
		}

		for(var i = 7; i < 10; i++){
			//print ("Setting Coordinate : " + i + k);
			Tile tile;
			tile = Instantiate(waterPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (17, i);
			tiles.Add(tile);
		}

		//18th
		for(var i = 0; i < 7; i++){
			//print ("Setting Coordinate : " + i + k);
			Tile tile;
			tile = Instantiate(groundPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (18, i);
			tiles.Add(tile);
		}

		for(var i = 7; i < 10; i++){
			//print ("Setting Coordinate : " + i + k);
			Tile tile;
			tile = Instantiate(waterPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (18, i);
			tiles.Add(tile);
		}

		//19th
		for(var i = 0; i < 7; i++){
			//print ("Setting Coordinate : " + i + k);
			Tile tile;
			tile = Instantiate(groundPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (19, i);
			tiles.Add(tile);
		}

		for(var i = 7; i < 10; i++){
			//print ("Setting Coordinate : " + i + k);
			Tile tile;
			tile = Instantiate(waterPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (19, i);
			tiles.Add(tile);
		}
	}
}
