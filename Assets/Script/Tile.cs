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
	public Color attackableColor;
	[SerializeField]
	public Color movableColor;

	int x;
	int y;

	public int X{
		get{ return x; }
	}

	public int Y {
		get { return y; }
	}

	public bool IsMovable
	{
		set
		{
			highlight.color = movableColor;
			highlight.gameObject.SetActive(value);
		}
		get { return highlight.gameObject.activeSelf && highlight.color == movableColor; }
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
		
	public void SetCoordinate(int x, int y){
		this.x = x;
		this.y = y;
	}

	public void OnClick(){
		if (map.FocusedUnit == null) {
			return;
		} else {
			if (IsMovable) {
				map.destinationTile = this;
				print ("Destination tile " + map.destinationTile.X + " " + map.destinationTile.Y);
				map.moveUnit (this, map.FocusedUnit);
				//print ("focused unit " + map.FocusedUnit.x + " " + map.FocusedUnit.y);
			}
		}
	}
		
}
