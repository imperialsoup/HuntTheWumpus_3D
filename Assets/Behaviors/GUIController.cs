using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GUIController : MonoBehaviour {
	//text variables
	public string prompterText, inputText, playerText, arrowText, gameText;
	Text promptGUI, inputGUI, playerGUI, arrowGUI, gameGUI;

	//controller variables
	InputController inputControl;
	GameController gameController;
	ImagePlaneController imgController;

	public Player player;
	public char gameState;
	InputMode inputMode;

	//text dictionary
	Dictionary<InputMode, string> textDictionary;

	//initialization
	void Start () {
		//set reference to GUI Texts
		promptGUI = GameObject.Find("Canvas/PrompterText").GetComponent<Text>();
		inputGUI = GameObject.Find("Canvas/InputText").GetComponent<Text>();
		playerGUI = GameObject.Find("Canvas/PlayerText").GetComponent<Text>();
		arrowGUI = GameObject.Find("Canvas/ArrowText").GetComponent<Text>();
		gameGUI = GameObject.Find("Canvas/GameText").GetComponent<Text>();

		//set reference to controller variables
		inputControl = GameObject.Find("GameController").GetComponent<InputController>();
		gameController = GameObject.Find("GameController").GetComponent<GameController>();
		imgController = GameObject.Find("GameController").GetComponent<ImagePlaneController>();

		//init text variables
		prompterText = "";
		inputText = "";
		playerText = "";
		gameText = "";
		arrowText = "";

		//init textDictionary
		textDictionary = new Dictionary<InputMode, string>();
		textDictionary.Add(InputMode.getMove, "(M)ove or (S)hoot?");
		textDictionary.Add(InputMode.getRoom, "Pick a room to move into");
		textDictionary.Add(InputMode.getArrowPath, "Pick a room to shoot your arrow through : ");
		textDictionary.Add(InputMode.won, "Aha! You got the Wumpus! ");
		textDictionary.Add(InputMode.lost, "You lost! ");
		textDictionary.Add(InputMode.wumpus, "Oops, bumped a Wumpus!");
		textDictionary.Add(InputMode.bat, "Zap--Super Bat snatch! Elsewhereville for you!");
		textDictionary.Add(InputMode.pit, "YYYIIIIEEEE . . . fell in a pit");
		textDictionary.Add(InputMode.getPlayAgain, "Play again? \n 1)New game, hazards in SAME place \n 2) New game, hazards in NEW places \n 3) Quit");
		}

	// Update is called once per frame
	void Update () {
		updateVars();
		updateText();
		setCanvasText();
		}

	//updates control variables
	void updateVars(){
		inputMode = inputControl.getMode();
		gameState = gameController.STATE;
		}

	//updates text variables based on control variables
	void updateText(){
		//update prompterText
		switch(inputMode){
			case InputMode.getMove:	prompterText = textDictionary[InputMode.getMove];
							break;
			case InputMode.getRoom:	if(inputControl.current != null){
										prompterText = textDictionary[InputMode.getRoom]
										+ "a) " + (inputControl.current.neighbors[0].id + 1)
										+ ", b) " + (inputControl.current.neighbors[1].id + 1)
										+ ", c) " + (inputControl.current.neighbors[2].id + 1);}
										break;
			case InputMode.getArrowPath: if(inputControl.current != null){
											prompterText = textDictionary[InputMode.getArrowPath]
											+ "a) " + (inputControl.current.neighbors[0].id + 1)
											+ ", b) " + (inputControl.current.neighbors[1].id + 1)
											+ ", c) " + (inputControl.current.neighbors[2].id + 1);}
											break;
			case InputMode.getPlayAgain:if(gameState == 'W')
											prompterText = textDictionary[InputMode.won] + textDictionary[InputMode.getPlayAgain];
										else if (gameState == 'L')
											prompterText = textDictionary[InputMode.lost] + textDictionary[InputMode.getPlayAgain];
											break;
			}

		//update inputText
		inputText = inputControl.input;
		//update playerText
		playerText = player.warningMessage;

		//update gameText
		if(imgController.BAT)
			gameText = textDictionary[InputMode.bat];
		if(imgController.PIT)
			gameText = textDictionary[InputMode.pit];
		if(imgController.WUMPUS)
			gameText = textDictionary[InputMode.wumpus];

		//update arrowText
		arrowText = player.arrowCount.ToString();
		}

	/*clear the game hazard text*/
	public void clearText(){
		gameText = "";
	}

	//sets the GUI texts from text variables
	void setCanvasText(){
		promptGUI.text = prompterText;
		inputGUI.text = inputText;
		playerGUI.text = playerText;
		arrowGUI.text = arrowText;
		gameGUI.text = gameText;
		}
}
