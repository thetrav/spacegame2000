using UnityEngine;
using System.Collections;

public class TargetCameraScript : MonoBehaviour {

	public TargetScript targetting;

	void FixedUpdate() {
		GameObject target = targetting.target;
		if(target == null) return;
		Vector3 pos = target.transform.position;
		Vector3 newPos = new Vector3(pos.x, pos.y, -5f);
		
		transform.position = newPos;
	}
}
