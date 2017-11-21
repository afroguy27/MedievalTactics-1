using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Tile : MonoBehaviour {

	[SerializeField]
	Map map;
	[SerializeField]
	public int cost;
	[SerializeField]
	public Image highlight;
	[SerializeField]
	Color attackableColor;

	int x;
	int y;

	public int X{
		get{ return x; }
	}

	public int Y {
		get { return y; }
	}
	 
	public bool IsAttackable
	{
		set
		{ 
			highlight.color = attackableColor;
			highlight.gameObject.SetActive(value);
		}
		get { return highlight.gameObject.activeSelf && highlight.color == attackableColor; }
	}

	public Unit Unit
	{
		get { return map.GetUnit(x, y); }
	}

	public Obstacles obst{
		get{ return map.GetObst (x, y);	}
	}
		
	public void SetCoordinate(int x, int y){
		this.x = x;
		this.y = y;
	}

	public void OnClick(){
		if (map.FocusedUnit == null) {
			return;
		} else {
			map.destinationTile = this;
			print ("Destination tile " + map.destinationTile.X + " " + map.destinationTile.Y);
			map.moveUnit (this, map.FocusedUnit);
			print ("forcused unit " + map.FocusedUnit.x + " " + map.FocusedUnit.y);
		}
	}

	// Use this for initialization
	void Start () {
		//GetComponent<Button>().onClick.AddListener(OnClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
