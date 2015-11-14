using UnityEngine;
using System;
using System.Linq;
using System.Collections;

public class RoomBehavior : MonoBehaviour {
	public int id;
	public Room room;
	bool hasPlayer, hasBat, hasWumpus, hasPit, hasArrow;
	InputController inputController;
	ImagePlaneController imgController;
	Transform trans;
	Renderer rend;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer>();
		trans = GetComponent<Transform>();
		imgController = GameObject.Find("GameController").GetComponent<ImagePlaneController>();

		id = Int32.Parse(gameObject.name.Substring(12));
		inputController = GameObject.Find("GameController").GetComponent<InputController>();
		}

	// Update is called once per frame
	void Update () {
		if (room != null)
			updateOccupants();
		}


	//updates appearance based on occupants
	void updateOccupants(){

		//if room has player, change color
		if(room.hasOccupantType(EntityType.player)){
			rend.material.color = Color.blue;
			GameObject.Find("Player").GetComponent<Transform>().position = trans.position;
		}
		else
			rend.material.color = Color.white;

		/*//if room has player and wmpus, show wumpus
		if(room.hasOccupantType(EntityType.wumpus)){
			rend.material.color = Color.red;
		}

		//if room has player and bat, show bat
		if(room.hasOccupantType(EntityType.bat)){ //&& room.hasOccupantType(EntityType.player)
			rend.material.color = Color.magenta;
		}

		//if room has player and pit, show pit
		if(room.hasOccupantType(EntityType.pit) ){
			rend.material.color = Color.black;
		}*/

	//real stuff
		//show arrow path
		if(inputController.tempPath.Count > 0 && inputController.tempPath.Contains(room)){
			if (inputController.arrowPath != null)
				rend.material.color = Color.green;
			else
				rend.material.color = Color.yellow;
		}

		//if room has player and bat, show bat
		if(room.hasOccupantType(EntityType.bat) && room.hasOccupantType(EntityType.player)){ //
			imgController.BAT = true;
			imgController.imgRoom = trans;
		}
		//if room has player and pit, show pit
		if(room.hasOccupantType(EntityType.pit) && room.hasOccupantType(EntityType.player)){ //
			imgController.PIT = true;
			imgController.imgRoom = trans;
		}
		//if room has player and wumpus, show wumpus
		if(room.hasOccupantType(EntityType.wumpus) && room.hasOccupantType(EntityType.player)){ //
			imgController.WUMPUS= true;
			imgController.imgRoom = trans;
		}
	}


}
