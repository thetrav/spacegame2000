using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class ControllerScript : MonoBehaviour {

	public GameObject ship;

	public bool left = false;
	public bool right = false;
	public bool up = false;
	public bool down = false;

	public bool changeTarget = false;


	// Use this for initialization
	void Start () {
		ship = gameObject;
	}

	
	// Update is called once per frame
	void Update () {
		
	}
}
