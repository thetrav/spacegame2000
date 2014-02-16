using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ThrustScript : MonoBehaviour {
	
	public delegate bool input();
	
	public class Thruster {
		public Thruster(input active, Vector2 direction, Vector2 position) {
			this.active = active;
			this.direction = direction;
			this.position = position;
		}
		
		public input active;
		public Vector2 direction;
		public Vector2 position;
		public float magnitude = 0f;
	}
	
	static Vector2 v2(float x, float y) {
		return new Vector2 (x, y);
	}
	
	public List<Thruster> thrusters;
	public ControllerScript controller;
	
	// Use this for initialization
	void Start () {
		controller = GetComponent<ControllerScript>();
		thrusters = new List<Thruster>() {
			new Thruster(delegate() {return controller.up;},  v2( 0.0f, 1.0f), v2( 0.0f, -1.2f)),
			new Thruster(delegate() {return controller.down;}, v2( 0.0f,-0.5f), v2( 0.0f,  1.2f)),
			new Thruster(delegate() {return controller.left;},  v2( 0.5f, 0.0f), v2(-1.0f, -1.2f)),
			new Thruster(delegate() {return controller.right;}, v2(-0.5f, 0.0f), v2( 1.0f, -1.2f))
		};
	}
	
	void updateThrusters() {
		foreach (Thruster thruster in thrusters) {
			if(thruster.active()) {
				thruster.magnitude = 0.1f;
			} else {
				thruster.magnitude = 0f;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		updateThrusters ();
	}
	
	Vector2 localToGlobal(Vector2 v2) {
		Vector3 v3 = transform.TransformPoint (v2);
		return new Vector2 (v3.x, v3.y);
	}
	
	void applyThrust(Vector2 force, Vector2 position) {
		rigidbody2D.AddForceAtPosition(force, position);
	}
	
	void applyThrusters() {
		Vector2 shipPos = transform.position;
		foreach (Thruster thruster in thrusters) {
			Vector2 pos = localToGlobal(thruster.position);
			Vector2 dir = localToGlobal(thruster.direction) - shipPos;
			Vector2 force = dir * thruster.magnitude;
			
			Debug.DrawLine(pos, pos - dir, Color.white, 0f, false);
			if(thruster.magnitude > 0) {
				applyThrust (force, pos);
				Debug.DrawLine(pos, pos - force, Color.red, 0f, false);
			}
		}
	}
	
	void FixedUpdate() {
		applyThrusters ();
	}

}
