using UnityEngine;
using System.Collections;

public class lookAtCenter : MonoBehaviour {
	Transform target, trans;
	// Use this for initialization
	void Start () {
		target = GameObject.Find("WumpusCave").GetComponent<Transform>();
		trans = GetComponent<Transform>();
	}

	// Update is called once per frame
	void Update () {
		trans.LookAt(target);
	}
}
