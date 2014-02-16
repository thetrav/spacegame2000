using UnityEngine;
using System.Collections;

public class KeyboardControllerScript : MonoBehaviour {


	public string left = "left";
	public string right = "right";
	public string up = "up";
	public string down = "down";
	public string changeTarget = "t";

	private ControllerScript control;

	// Use this for initialization
	void Start () {
		control = GetComponent<ControllerScript>();
	}
	
	// Update is called once per frame
	void Update () {
		control.left = Input.GetKey (left);
		control.right = Input.GetKey (right);
		control.up = Input.GetKey (up);
		control.down = Input.GetKey (down);
		control.changeTarget = Input.GetKeyDown (changeTarget);
	}
	
}
