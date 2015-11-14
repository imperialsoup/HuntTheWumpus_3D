using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum InputMode{
	getMove,
	getRoom,
	getArrowPath,
	getPlayAgain,
	won,
	lost,
	bat,
	wumpus,
	pit
	}


public class InputController : MonoBehaviour {
	public InputMode mode;
	public string input;

	public Player player;	//assigned  by Game controller
	public Room current;

	// Use this for initialization
	void Start () {
		mode = InputMode.getMove;
		current = null;
		input = "";
		}

	// Update is called once per frame
	void Update () {
		getInput();
		updateMode();
		}

	void getInput(){
		switch(mode){
			case InputMode.getMove:	getMove();
							break;
			case InputMode.getRoom:	getRoom();
							break;
			case InputMode.getArrowPath: getArrowPath();
							break;
			case InputMode.getPlayAgain: getPlayAgain();
							break;
			}
		}

	/*gets player move (M)ove, (S)hoot, (Q)uit*/
	bool move;
	void getMove(){
		move = false;
		if (Input.GetKeyDown(KeyCode.M)){
			input = "M";
			move = true;
			mode = InputMode.getRoom;
			}
		if (Input.GetKeyDown(KeyCode.S)){
			input = "S";
			move = true;
			mode = InputMode.getArrowPath;
			}
		if (Input.GetKeyDown(KeyCode.Q)){
			input = "Q";
			move = true;
			}
		//clear everything
		if (move){
			selectedRoom = null;
            arrowPath = null;
            tempPath.Clear();
			move = false;
			}
		}

	public Room selectedRoom;
	/*gets a room selection and stores it in selectedRoom*/
	void getRoom(){
		selectedRoom = null;
		updateCurrentRoom();

		if (Input.GetKeyDown(KeyCode.A)){
			input = "A) " + (current.neighbors[0].id + 1);
            selectedRoom = current.neighbors[0];
			}
		if (Input.GetKeyDown(KeyCode.B)){
			input = "B) " + (current.neighbors[1].id + 1);
            selectedRoom  = current.neighbors[1];
			}
		if (Input.GetKeyDown(KeyCode.C)){
			input = "C) : " + (current.neighbors[2].id + 1);
            selectedRoom = current.neighbors[2];
			}
		if (Input.GetKeyDown(KeyCode.D)){
			input = "D) : Done";
			selectedRoom = player.location;
			}
		}

	/*updates the current room*/
	void updateCurrentRoom(){
		if (mode == InputMode.getRoom || mode == InputMode.getMove)
			current = player.location;
		if (mode == InputMode.getArrowPath){
			if (tempPath.Count == 0)
				current = player.location;	 //TODO : change to last chosen arrow room
			else
				current = tempPath[tempPath.Count-1];
			}

		}

	public List<Room> arrowPath = null;
	public List<Room> tempPath = new List<Room>(5);

	/*builds the list of rooms to shoot an arrow through*/
	void getArrowPath(){
		getRoom();
		//a selection is made and there is room for another room
		if(selectedRoom != null && selectedRoom != player.location && tempPath.Count < 5){
			tempPath.Add(selectedRoom);
			UnityEngine.Debug.Log("Added room " + selectedRoom.id + "to arrow path.");
			updateCurrentRoom();
			selectedRoom = null;
			}
		//done selecting rooms or path capacity is full
		if(selectedRoom == player.location || tempPath.Count == 5){
			arrowPath = tempPath;
			selectedRoom = null;
			}
		}

	public bool choice;
	void getPlayAgain(){
		choice = false;
		if (Input.GetKeyDown(KeyCode.Alpha1)){
			input = "1";
			choice = true;
			}
		if (Input.GetKeyDown(KeyCode.Alpha2)){
			input = "2";
			choice = true;
			}
		if (Input.GetKeyDown(KeyCode.Alpha3)){
			input = "3";
			choice = true;
			}
		//clear everything
		if (choice){
			selectedRoom = null;
			arrowPath = null;
			tempPath.Clear();
			}
		}


	void updateMode(){

		}

	public InputMode getMode(){
		return mode;
	}
}
