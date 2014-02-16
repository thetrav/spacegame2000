using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetScript : MonoBehaviour {
	
	public ControllerScript control;
	public GameObject target;
	
	private int currentTarget = 0;
	public List<GameObject> potentialTargets;

	// Use this for initialization
	void Start () {
		control = GetComponent<ControllerScript>();
		WeaponScript weapon = GetComponentInChildren<WeaponScript>();
		weapon.targetting = this;
	}

	private void updateTarget() {
		if (control.changeTarget && potentialTargets.Count > 0) {
			currentTarget = currentTarget + 1;
			if (currentTarget >= potentialTargets.Count) {
				currentTarget = 0;
			}
			target = potentialTargets [currentTarget];
		}
	}

	void Update () {
		updateTarget ();
	}
}
