using UnityEngine;
using System.Collections;

public class ShotScript : MonoBehaviour {

	//These values supplied by WeaponScript
	public GameObject firer;
	public float timeToLive = 0f;
	public float damage = 0f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (timeToLive > 0) {
			timeToLive -= Time.deltaTime;
		}
	}

	void FixedUpdate() {
		if (timeToLive <= 0) {
			Destroy (gameObject);
		}
	}
}
