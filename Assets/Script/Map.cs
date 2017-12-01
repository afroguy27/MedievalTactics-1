using System.Collections;
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
	Transform UnitContainer;

	GameObject touchBlocker;


	string atkStr=" ";
	string healthStr=" ";
	string rangeStr=" ";
	public Text healthtxt;
	public Text atktxt;
	public Text rangetxt;



	public Unit FocusedUnit;
	public bool unitisAttacking;
	public Tile destinationTile;

	List<Tile> tiles = new List<Tile>();
	List<Tile> highlightedMoveTiles = new List<Tile>();
	List<Tile> highlightedAttackTiles = new List<Tile>();

	//public Unit FocusedUnit
	//{
	//	get { return UnitContainer.GetComponentsInChildren<Unit>().FirstOrDefault(x => x.isFocused);}
	//}

	public void GenerateMap(){
		
		foreach (var tile in tiles) {
			Destroy (tile.gameObject);
		}
			
		for (var i = 0; i < 5; i++) {
			for(var k = 0; k < 10; k++){
				//print ("Setting Coordinate : " + i + k);
				Tile tile;
				tile = Instantiate(groundPrefab);
				tile.gameObject.SetActive(true);
				tile.transform.SetParent(transform);
				tile.SetCoordinate (i, k);
				tiles.Add(tile);
			}
		}

		for (var i = 0; i < 7; i++) {
			//print ("Setting Coordinate : " + i + 5);
			Tile tile;
			tile = Instantiate(groundPrefab);
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

		for (var i = 5; i < 10; i++) {
			//print ("Setting Coordinate : " + i + 9);
			Tile tile;
			tile = Instantiate(groundPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (9, i);
			tiles.Add(tile);
		}

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

		for (var i = 2; i < 10; i++) {
			//print ("Setting Coordinate : " + i + 10);
			Tile tile;
			tile = Instantiate(groundPrefab);
			tile.gameObject.SetActive(true);
			tile.transform.SetParent(transform);
			tile.SetCoordinate (10, i);
			tiles.Add(tile);
		}

		for (var i = 11; i < 13; i++) {
			for(var k = 0; k < 10; k++){
				//print ("Setting Coordinate : " + i + k);
				Tile tile;
				tile = Instantiate(groundPrefab);
				tile.gameObject.SetActive(true);
				tile.transform.SetParent(transform);
				tile.SetCoordinate (i, k);
				tiles.Add(tile);
			}
		}


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

		for(var i = 0; i < 8; i++){
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

	public void PutUnit(int x, int y, Unit Prefab, bool isEnemy){
		//print ("putting unit " + Prefab.name);
		var unit = Instantiate (Prefab);
		unit.isEnemy = isEnemy;
		unit.gameObject.SetActive (true);
		unit.transform.SetParent (UnitContainer);
		unit.transform.position = tiles.First (c => c.X == x && c.Y == y).transform.position;
		unit.x = x;
		unit.y = y;

	}
	public void PutObst(int x, int y, Obstacles Prefab){
		var obstacle = Instantiate (Prefab);
		obstacle.gameObject.SetActive (true);
		obstacle.transform.SetParent (UnitContainer);
		obstacle.transform.position = tiles.First (c => c.X == x && c.Y == y).transform.position;
		obstacle.x = x;
		obstacle.y = y;
	}


	public Tile GetTile(int x, int y)
	{
		return tiles.First(c => c.X == x && c.Y == y);
	}

	public Unit GetUnit(int x, int y)
	{
		return UnitContainer.GetComponentsInChildren<Unit>().FirstOrDefault(u => u.x == x && u.y == y);
	}

	public Obstacles GetObst(int x, int y){
		return UnitContainer.GetComponentsInChildren<Obstacles>().FirstOrDefault(u => u.x == x && u.y == y);
	}

	public void AttackTo(Unit fromUnit, Unit toUnit)
	{
		toUnit.loseHealth (fromUnit.ATK);
		FocusedUnit.isMoved = true;
	}

	public void highlightAttackable(int range, int x, int y){
		// print ("range is " + range);
		if (range == 0) {
			highlight (x, y);
			highlightedAttackTiles.Add (GetTile(x,y));
			//print (x + ", " + y + " is attackable.");
		} else {
			highlight (x, y);
			highlightedAttackTiles.Add (GetTile(x,y));
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

		// print ("range is " + range);
		if (movement == 0) {
			highlight (x, y);
			highlightedMoveTiles.Add (GetTile(x,y));
			//print (x + ", " + y + " is attackable.");
		} else {
			highlight (x, y);
			highlightedMoveTiles.Add (GetTile(x,y));
			//print (x + ", " + y + " is attackable.");
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

	public void highlight(int x, int y){
		if (GetTile (x, y) == null) {
			return;
		} else {
			GetTile (x, y).highlight.gameObject.SetActive(true);
		}
	}

	public void clearHighlightAttack(){
		for (int i = 0; i < highlightedAttackTiles.Count; i++) {
			highlightedAttackTiles [i].highlight.gameObject.SetActive (false);
		}
		highlightedAttackTiles.Clear ();
	}

	public void clearHighlightMove(){
		for (int i = 0; i < highlightedMoveTiles.Count; i++) {
			//print ("debug " + highlightedMoveTiles [i].X);
			highlightedMoveTiles [i].highlight.gameObject.SetActive (false);
		}
		highlightedMoveTiles.Clear ();
	}

	public void moveUnit(Tile target, Unit Focus){
		if ((((target.X+1) == (Focus.x) || (target.X-1) == (Focus.x)))&&target.Y==Focus.y||(((target.Y+1) == (Focus.y) || (target.Y-1) == (Focus.y))&&target.X==Focus.x)) {
			clearHighlightMove ();
			clearHighlightAttack ();
			print ("debug " + Focus.x + " " + Focus.y);
			print (GetUnit (Focus.x, Focus.y).gameObject.name);
			GetUnit (Focus.x, Focus.y).transform.position = tiles.First (c => c.X == target.X && c.Y == target.Y).transform.position;
			print ("debug " + Focus.x + " " + Focus.y);
			FocusedUnit.x = target.X;
			FocusedUnit.y = target.Y;
			highlightAttackable (FocusedUnit.range, FocusedUnit.x, FocusedUnit.y);
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
		healthtxt.text = "Health: " + healthStr;
		atktxt.text = "Attack: " + atkStr;
		rangetxt.text = "Range: " + rangeStr;
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
		healthStr = a;
	}

	public void setAtkStr(string a){
		atkStr = a;
	}

	public void setRangeStr(string a){
		rangeStr = a;
	}



}
