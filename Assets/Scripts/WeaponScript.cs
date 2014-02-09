using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public float rotation = 0.0f;
	public float aimSpeed = 0.5f;

	// Update is called once per frame
	void Update () {
		if (Input.GetKey ("[")) {
				rotation = aimSpeed;
		} else if (Input.GetKey ("]")) {
				rotation = aimSpeed * -1.0f;
		} else {
				rotation = 0.0f;
		}
	}

	void FixedUpdate() {
		transform.Rotate (new Vector3 (0, 0, 1), rotation);
	}
}
