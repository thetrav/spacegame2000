using UnityEngine;
using System.Collections;

public class NavigationScript : MonoBehaviour {

	public ControllerScript controller;
	public TargetScript target;
	
	private float targetDistance = 20f;
	private float margin = 2f;
	
	public float maxTurn = 5;
	public float maxVel = 5;

	// Use this for initialization
	void Start () {
		controller = GetComponent<ControllerScript>();
		target = GetComponent<TargetScript>();
	}
	
	// Update is called once per frame
	void Update () {
		float angle = target.angleToTarget(transform);
		float distance = target.distanceFromTarget(transform);
		float angularVel = rigidbody2D.angularVelocity;
		
//		Debug.Log ("angularVel:"+angularVel);
		
		controller.left = angle < 0 && angularVel < maxTurn;
		controller.right = angle > 0 && angularVel > -maxTurn;
	
		controller.up = angle > -45 && angle < 45 && distance > targetDistance + margin;
		controller.down = angle < -45 && angle > 45 && distance < targetDistance - margin;
	}
}
