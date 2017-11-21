using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	Obstacles RockPrefab;
	[SerializeField]
	Obstacles TreePrefab;

	// Use this for initialization
	IEnumerator Start () {
		map.GenerateMap();
		yield return null;

		KingPrefab.gameObject.SetActive(false);
		EnemyKingPrefab.gameObject.SetActive(false);
		ArcherPrefab.gameObject.SetActive (false);

		map.PutUnit (0, 4, KingPrefab, false);
		map.PutUnit (0, 6, ArcherPrefab, false);
		map.PutUnit (1, 5, SwordmanPrefab, false);
		map.PutUnit (1,7, SwordmanPrefab, false);

		map.PutUnit (1, 4, EnemyKingPrefab, true);
		map.PutUnit (19, 6, EnemyArcherPrefab, true);
		map.PutUnit (18, 5, EnemySwordmanPrefab, true);
		map.PutUnit (18, 6, EnemySwordmanPrefab, true);

		map.PutObst (5, 5, TreePrefab);
		map.PutObst (10, 5, TreePrefab);
		map.PutObst (9, 8, TreePrefab);
		map.PutObst (11, 3, TreePrefab);

		map.PutObst (4, 3, RockPrefab);
		map.PutObst (2, 1, RockPrefab);
		map.PutObst (1, 9, RockPrefab);
		map.PutObst (14, 2, RockPrefab);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
