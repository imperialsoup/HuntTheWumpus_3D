using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {
	Renderer rend;
	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer>();
	}

	// Update is called once per frame
	void Update () {
		if(rend.enabled)
			gameObject.GetComponent<Transform>().Rotate(Vector3.up, 5, Space.World);
	}
}
