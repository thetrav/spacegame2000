using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public string left = "[";
	public string right = "]";
	public string fire = "p";

	public float rotation = 0.0f;
	public float aimSpeed = 0.5f;
	public float fireRate = 1f;
	public float timeToNextShot = 0f;
	public float shotPower = 1000f;
	public float shotLife = 5f;
	public float damage = 10f;

	private bool attacking = false;

	public Transform shotPrefab;

	// Update is called once per frame
	void Update () {
		if (Input.GetKey (left)) {
			rotation = aimSpeed;
		} else if (Input.GetKey (right)) {
			rotation = aimSpeed * -1.0f;
		} else {
			rotation = 0.0f;
		}

		if (Input.GetKey (fire) && timeToNextShot <= 0) {
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
		shotPos.transform.Translate( transform.position);
		shotPos.transform.Rotate (transform.eulerAngles);
		//set owner as this
		ShotScript shotScript = shotPos.gameObject.GetComponent<ShotScript> ();
		shotScript.firer = transform.parent.gameObject;
		shotScript.timeToLive = shotLife;
		shotScript.damage = damage;
		//add force to transform
		Vector3 shotForce = shotPos.up * shotPower;
//		Debug.DrawLine(shotPos.position, shotPos.position + shotForce, Color.red, 0f, false);
		shotPos.rigidbody2D.AddForce (shotForce);
	}

	void FixedUpdate() {
		transform.Rotate (new Vector3 (0, 0, 1), rotation);

		if (attacking) {
			timeToNextShot = fireRate;
			fireShot();
		}
	}
}
