using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public Vector2 accel = new Vector2(1, 1);
	private Vector2 movement;
	private float bearing = 180;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");

		movement = new Vector2(
			accel.x * inputX,
			accel.y * inputY);

		Vector2 velocity = rigidbody2D.velocity;

		float mag = velocity.magnitude;

		if (mag > 0) {
			float angle = Mathf.Atan2 (velocity.x, velocity.y);
			bearing = 180 - (57.295f * angle);
		}
	}

	void FixedUpdate() {
		rigidbody2D.AddForce(movement);
		transform.localEulerAngles = new Vector3 (0, 0, (int)bearing);
	}
}
