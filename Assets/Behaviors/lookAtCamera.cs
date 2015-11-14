
using UnityEngine;
using System.Collections;

public class lookAtCamera : MonoBehaviour {
	public Transform target, trans;
	void Start () {
		target = GameObject.Find("Main Camera").GetComponent<Transform>();
		trans = gameObject.GetComponent<Transform>();
	}

	void Update () {
		Vector3 relativePos = target.position - trans.position;
		trans.rotation = Quaternion.LookRotation(-relativePos);
		//trans.LookAt(target.transform);
	}
}
