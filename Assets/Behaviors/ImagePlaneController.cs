using UnityEngine;
using System.Collections;

public class ImagePlaneController : MonoBehaviour {
	public Renderer rend;
	public bool BAT, WUMPUS, PIT;

	GameObject imagePlaneGO;
	public Transform imgRoom;

	// Use this for initialization
	void Start () {
		imagePlaneGO = GameObject.Find("batPlane");
		clearFlags();

	}

	// Update is called once per frame
	void Update () {
		if (BAT){
			imagePlaneGO = GameObject.Find("batPlane");
		}
		if (PIT){
			imagePlaneGO = GameObject.Find("pitPlane");
		}
		if (WUMPUS){
			imagePlaneGO = GameObject.Find("wumpusPlane");
		}
		if (BAT || WUMPUS || PIT){
			imagePlaneGO.GetComponent<Transform>().position = imgRoom.position;
			imagePlaneGO.GetComponent<Transform>().Translate(Vector3.up * 5);
			imagePlaneGO.GetComponent<Renderer>().enabled = true;
		}
	}

	public void clearFlags(){
		BAT = WUMPUS = PIT = false;
		imagePlaneGO.GetComponent<Renderer>().enabled = false;
	}
}
