using UnityEngine;
using System.Collections;

public class DamageScript : MonoBehaviour {

	public float health = 1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D otherCollider) {
		ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript> ();
		if (shot == null || shot.firer == gameObject) {
			return;
		} else {
			Debug.Log ("shot hit gameObject="+shot.firer +" doesn't match this one"+gameObject);
			health -= shot.damage;
			Destroy(shot.gameObject);
		}
	}

	void FixedUpdate() {
		if (health <= 0) {
			Destroy(gameObject);
		}
	}
}
