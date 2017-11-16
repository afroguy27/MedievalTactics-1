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
	Unit ArcherPrefab;
	[SerializeField]
	Unit EnemyArcherPrefab;

	// Use this for initialization
	IEnumerator Start () {
		map.GenerateMap();
		yield return null;

		KingPrefab.gameObject.SetActive(false);
		EnemyKingPrefab.gameObject.SetActive(false);
		ArcherPrefab.gameObject.SetActive (false);

		map.PutUnit (0, 4, KingPrefab, false);
		map.PutUnit (0, 6, ArcherPrefab, false);
		//map.PutUnit ();
		//map.PutUnit ();

		map.PutUnit (1, 4, EnemyKingPrefab, true);
		map.PutUnit (19, 6, EnemyArcherPrefab, true);
		//map.PutUnit ();
		//map.PutUnit ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
