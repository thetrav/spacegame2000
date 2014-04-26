using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetScript : MonoBehaviour {
	
	public ControllerScript control;
	public GameObject target;
	
	private int currentTarget = 0;
	public List<GameObject> potentialTargets;
//	private DebugScript debug;
	
	
	public float angleToTarget(Transform from) {
		Vector3 pos = from.position;
		Vector3 tPos = target.transform.position;
		
		Quaternion quat  = Quaternion.AngleAxis(from.eulerAngles.z * -1, Vector3.forward);
		Vector2 toTarg = quat * (tPos - pos).normalized;
		
		float angle = Vector2.Angle(Vector2.up, toTarg);
		if(toTarg.x < 0) angle = angle * -1;
		return angle;
	}
	
	public float distanceFromTarget(Transform from) {
	  return Vector2.Distance(from.position, target.transform.position);
	}

	// Use this for initialization
	void Start () {
//		debug = GetComponent <DebugScript>();
		control = GetComponent<ControllerScript>();
		WeaponScript weapon = GetComponentInChildren<WeaponScript>();
		weapon.targetting = this;
		control.changeTarget = true;
		updateTarget();
		control.changeTarget = false;
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
	
	void FixedUpdate() {
		
//		Vector3 pos = transform.position;
//		Vector3 tPos = target.transform.position;
//		Quaternion quat  = Quaternion.AngleAxis(transform.eulerAngles.z * -1, Vector3.forward);
//		Vector2 toTarg = quat * (tPos - pos);
//		
//		Debug.DrawLine(pos, toTarg, Color.yellow, 0f, false);
	}
}
