using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour {

	public string up;
	public string down;
	public string left;
	public string right;

	public Dictionary<String, String> controls;

	public class Thruster {
		public Thruster(string name, Vector2 direction, Vector2 position) {
			this.name = name;
			this.direction = direction;
			this.position = position;
		}

		public string name;
		public Vector2 direction;
		public Vector2 position;
		public float magnitude = 0f;
	}

	static Vector2 v2(float x, float y) {
		return new Vector2 (x, y);
	}

	public List<Thruster> thrusters;

	// Use this for initialization
	void Start () {
		Debug.Log ("up=" + up);
		thrusters = new List<Thruster>() {
			new Thruster(up,  v2( 0.0f, 1.0f), v2( 0.0f, -1.2f)),
			new Thruster(down, v2( 0.0f,-0.5f), v2( 0.0f,  1.2f)),
			new Thruster(left,  v2( 0.5f, 0.0f), v2(-1.0f, -1.2f)),
			new Thruster(right, v2(-0.5f, 0.0f), v2( 1.0f, -1.2f))
		};
	}

	void updateThrusters() {
		foreach (Thruster thruster in thrusters) {
			if(Input.GetKey (thruster.name)) {
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
