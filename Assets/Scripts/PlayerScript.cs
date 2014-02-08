using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour {

	public class Thruster {
		public Thruster(string name, Vector2 direction, Vector2 position, Vector2 control) {
			this.name = name;
			this.direction = direction;
			this.position = position;
			this.control = control;
		}

		public string name;
		public Vector2 direction;
		public Vector2 position;
		public float magnitude = 0f;
		public Vector2 control;
	}

	static Vector2 v2(float x, float y) {
		return new Vector2 (x, y);
	}

	public List<Thruster> thrusters = new List<Thruster>() {
		new Thruster("main",  v2( 0.0f, 1.0f), v2( 0.0f, -1.2f), v2( 0,  1)),
		new Thruster("retro", v2( 0.0f,-0.5f), v2( 0.0f,  1.2f), v2( 0, -1)),
		new Thruster("left",  v2( 0.5f, 0.0f), v2(-1.0f, -1.2f), v2( 1,  0)),
		new Thruster("right", v2(-0.5f, 0.0f), v2( 1.0f, -1.2f), v2(-1,  0))
	};

	// Use this for initialization
	void Start () {
	
	}

	bool matchDir(float a, float b) {
		return (a==0 && b == 0) || (a < 0 && b < 0) || (a > 0 && b > 0);
	}

	void updateThrusters(Vector2 c) {
		foreach (Thruster thruster in thrusters) {
			Vector2 tc = thruster.control;
			if(matchDir (c.x, tc.x) && matchDir (c.y, tc.y)) {
				thruster.magnitude = 0.1f;
			} else {
				thruster.magnitude = 0f;
			}
		}
	}

	// Update is called once per frame
	void Update () {
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");

		updateThrusters (v2(inputX, inputY));
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
