using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

//	public Vector2 accel = new Vector2(1, 1);
	public Vector2 mainThrustPower;
	public Vector2 mainThrustLocation;

	public Vector2 retroThrustPower;
	public Vector2 retroThrustLocation;

	public Vector2 leftThrustPower;
	public Vector2 leftThrustLocation;

	public Vector2 rightThrustPower;
	public Vector2 rightThrustLocation;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");


		//calculate position and angle of main thruster
		Vector2 pos = transform.position;

		GameObject mainThrust = GameObject.Find("Main Thrust");

		mainThrustLocation = mainThrust.transform.position;

		GameObject retroThrust = GameObject.Find("Retro Thrust");
		retroThrustLocation = retroThrust.transform.position;

		if(inputY > 0) {
			mainThrustPower = (pos - mainThrustLocation).normalized;
		} else { 
			mainThrustPower = Vector2.zero; 
		}
		if(inputY < 0) {
			retroThrustPower = (pos - retroThrustLocation).normalized / 2;
		} else { 
			retroThrustPower = Vector2.zero; 
		}

		GameObject leftThrust = GameObject.Find("Left Thrust");
		leftThrustLocation = leftThrust.transform.position;
		
		if(inputX < 0) {
			leftThrustPower = (mainThrustLocation - leftThrustLocation).normalized / 2;
		} else { 
			leftThrustPower = Vector2.zero; 
		}

		GameObject rightThrust = GameObject.Find("Right Thrust");
		rightThrustLocation = rightThrust.transform.position;
		
		if(inputX > 0) {
			rightThrustPower = (mainThrustLocation - rightThrustLocation).normalized / 2;
		} else { 
			rightThrustPower = Vector2.zero; 
		}
		//calculate position and angle of manouver thruster

	}

	void FixedUpdate() {
		rigidbody2D.AddForceAtPosition(mainThrustPower, mainThrustLocation);
		rigidbody2D.AddForceAtPosition(retroThrustPower, mainThrustLocation);
		rigidbody2D.AddForceAtPosition(leftThrustPower, mainThrustLocation);
		rigidbody2D.AddForceAtPosition(rightThrustPower, mainThrustLocation);
	}
}
