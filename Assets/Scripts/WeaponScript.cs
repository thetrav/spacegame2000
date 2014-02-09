using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public float rotation = 0.0f;
	public float aimSpeed = 0.5f;
	public float fireRate = 1f;
	public float timeToNextShot = 0f;
	public float shotPower = 100f;

	private bool attacking = false;

	public Transform shotPrefab;

	// Update is called once per frame
	void Update () {
		if (Input.GetKey ("[")) {
			rotation = aimSpeed;
		} else if (Input.GetKey ("]")) {
			rotation = aimSpeed * -1.0f;
		} else {
			rotation = 0.0f;
		}

		if (Input.GetKey ("p") && timeToNextShot <= 0) {
			attacking = true;
		} else {
			attacking = false;
		}

		if (timeToNextShot > 0) {
			timeToNextShot -= Time.deltaTime;
		}
	}

	private void fireShot() {
		//create new sprite at transform
		Transform shotPos = Instantiate (shotPrefab) as Transform;
		shotPos.transform.position = transform.position;
		shotPos.transform.eulerAngles = transform.eulerAngles;
		//set owner as this
		ShotScript shotScript = shotPos.gameObject.GetComponent<ShotScript> ();
		shotScript.firer = this;
		//add force to transform
		shotPos.rigidbody2D.AddForce (transform.TransformPoint (new Vector2(0,1)).normalized * shotPower);
	}

	void FixedUpdate() {
		transform.Rotate (new Vector3 (0, 0, 1), rotation);

		if (attacking) {
			timeToNextShot = fireRate;
			fireShot();
		}
	}
}
