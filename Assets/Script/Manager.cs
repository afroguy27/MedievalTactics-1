using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

	[SerializeField]
	Map map;
	[SerializeField]
	Unit KingPrefab;
	[SerializeField]
	Unit EnemyKingPrefab;
	[SerializeField]
	Unit SwordmanPrefab;
	[SerializeField]
	Unit EnemySwordmanPrefab;
	[SerializeField]
	Unit ArcherPrefab;
	[SerializeField]
	Unit EnemyArcherPrefab;
	[SerializeField]
	Unit CatapultPrefab;
	[SerializeField]
	Unit CatapultRedPrefab;
	[SerializeField]
	Unit ScoutPrefab;
	[SerializeField]
	Unit ScoutRedPrefab;
	[SerializeField]
	Obstacles RockPrefab;
	[SerializeField]
	Obstacles TreePrefab;

	private Button button; //button instance
	Unit unit;
	public MainMenu menu;
	public int level;

	// Use this for initialization
	IEnumerator Start () {
		
		
		yield return null;

		KingPrefab.gameObject.SetActive(false);
		EnemyKingPrefab.gameObject.SetActive(false);
		ArcherPrefab.gameObject.SetActive (false);
		EnemyArcherPrefab.gameObject.SetActive (false);
		SwordmanPrefab.gameObject.SetActive (false);
		EnemySwordmanPrefab.gameObject.SetActive (false);


		map.PutUnit (0, 5, KingPrefab, false);
		map.PutUnit (0, 6, ArcherPrefab, false);
		map.PutUnit (1, 5, SwordmanPrefab, false);
		map.PutUnit (1, 6, SwordmanPrefab, false);
		map.PutUnit (0, 7, CatapultPrefab, false);
		map.PutUnit (1, 7, ScoutPrefab, false);

		map.PutUnit (19, 5, EnemyKingPrefab, true);
		map.PutUnit (19, 6, EnemyArcherPrefab, true);
		map.PutUnit (18, 5, EnemySwordmanPrefab, true);
		map.PutUnit (18, 6, EnemySwordmanPrefab, true);
		map.PutUnit (19, 4, CatapultRedPrefab, true);
		map.PutUnit (18, 4, ScoutRedPrefab, true);

		//Sets the enemy sides isMoved to true so you can't use them
		for (int i = 0; i < map.unitsEnemy.Count; i++) {
			unit = map.unitsEnemy [i];
			unit.isMoved = true;
		}
	}

	void Awake () {
		if (level == 1)
			map.GenerateMap ();
		else if (level == 2)
			map.GenerateMap2 ();
	}
		
}
