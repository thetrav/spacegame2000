using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour {

	public TargetScript targetting;

	public float rotation = 0.0f;
	public float aimSpeed = 0.3f;
	public float fireRate = 1f;
	public float timeToNextShot = 0f;
	public float shotPower = 1000f;
	public float shotLife = 5f;
	public float damage = 10f;
	public float aimTollerance = 20.0f;

	private bool attacking = false;

	public Transform shotPrefab;
	
	
	private float trackTarget(GameObject target) {
		Vector3 pos = transform.position;
		Vector3 tPos = target.transform.position;
		
		Quaternion quat  = Quaternion.AngleAxis(transform.eulerAngles.z * -1, Vector3.forward);
		Vector2 toTarg = quat * (tPos - pos).normalized;
		
		float angle = Vector2.Angle(Vector2.up, toTarg);
		if(toTarg.x < 0) angle = angle * -1;
		
//		Debug.Log ("toTarg:"+ toTarg + " angle: "+angle);
		
//		Debug.DrawLine (Vector3.zero, pos, Color.blue, 0f, false);
//		Debug.DrawLine (Vector3.zero, tPos, Color.red, 0f, false);
//		Debug.DrawLine (Vector3.zero, toTarg, Color.yellow, 0f, false);
//		Debug.DrawLine (Vector3.zero, Vector3.up, Color.green, 0f, false);
		
		if (angle < aimSpeed  * -1) {
			return aimSpeed;
		} else if (angle > aimSpeed) {
			return aimSpeed * -1.0f;
		} else {
			return angle;
		}
	}

	// Update is called once per frame
	void Update () {
		if(targetting.target == null) return;
		
		rotation = trackTarget(targetting.target);
		
//		Debug.Log ("update rot:"+rotation);
		attacking = (rotation * rotation <= 0.1f && timeToNextShot <= 0);
	
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
//		Debug.Log ("rotation: "+rotation);
		transform.Rotate (new Vector3 (0, 0, 1), rotation);

		if (attacking) {
			timeToNextShot = fireRate;
			fireShot();
		}
	}
}
