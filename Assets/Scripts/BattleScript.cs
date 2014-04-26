using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleScript : MonoBehaviour {
	//supplied by scene
	public Transform enemyPrefab;
	public Transform playerPrefab;
	
	public GameObject targetCamera;

	//inited at start
	public List<GameObject> enemies = new List<GameObject>();
	public List<GameObject> allies = new List<GameObject>();
	public GameObject player;

	void initPlayer() {
		player = (Instantiate (playerPrefab) as Transform).gameObject;
		TargetScript targetting = player.GetComponent<TargetScript>();
		targetting.potentialTargets = enemies;
		targetCamera.GetComponent<TargetCameraScript>().targetting = targetting;
		allies.Add (player);
	}

	void newEnemy(float x, float y) {
		GameObject enemy = (Instantiate (enemyPrefab) as Transform).gameObject;
		enemy.transform.Translate( new Vector3(x,y,1f));
		enemy.GetComponent<TargetScript>().potentialTargets = allies;
		enemies.Add (enemy);
		
	}

	// Use this for initialization
	void Start () {
		initPlayer ();

		newEnemy (20,20);
		newEnemy (25,20);
		newEnemy (20,25);
		newEnemy (-20,20);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
