using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
    //Cave stuff
    public Cave wumpusCave;
    List<Room> rooms;

    //Game Entities
    public Player player;
    Wumpus wumpus;
    List<Bat> bats;
    List<Pit> pits;

    int hazard_cap;
    public char STATE  = 'u';
    public int[] positions;


    InputController inputController;
    GUIController guiController;
    ImagePlaneController imgController;

    //holds the room prefab
    public Transform roompb,  imageplanePB;

    void Start(){
        hazard_cap = 2;
        wumpusCave = new Cave();
        player = new Player("Hunter");
        wumpus = new Wumpus("Wumpy");
        bats = new List<Bat>(hazard_cap);
        pits = new List<Pit>(hazard_cap);

       //init bats & pits
       for (int i = 0; i < hazard_cap; i++ ){
           Bat bat = new Bat(i.ToString());
           bats.Add(bat);
           Pit pit = new Pit(i);
           pits.Add(pit);
            }

       positions = new int[6];
       setup(1);

       //assign player references to controllers
       //*I don't like it but I run into Object reference not set to an instance errors otherwise..
       inputController = GetComponent<InputController>();
       guiController =  GameObject.Find("Canvas").GetComponent<GUIController>();
       imgController = GetComponent<ImagePlaneController>();
       inputController.player = player;
       guiController.player = player;
       guiController.gameState = STATE;

       //assign Room prefabs to a room DAO
       Transform Rooms = GameObject.Find("WumpusCave/Rooms").GetComponent<Transform>();
       UnityEngine.Debug.Log(Rooms.childCount);
       for(int i = 0; i < Rooms.childCount; i++)
           Rooms.GetChild(i).GetComponent<RoomBehavior>().room = wumpusCave.rooms[i];

        }

    void Update(){
    
        processInput();
        updateGameState();
        handleHazards();
        //player.sense();
        }

    void processInput(){
        switch(inputController.mode){
                                    //if a choice is made, reset things
            case InputMode.getMove:
                                    break;
            case InputMode.getRoom:
                                    if (inputController.selectedRoom != null){
                                        imgController.clearFlags();
                                        guiController.clearText();
                                        player.sense();
                                        player.move(inputController.selectedRoom);
                                        inputController.mode = InputMode.getMove;   //only here!! will gamecotnroller set inputcontrol's mode
                                        }

                                    break;
            case InputMode.getArrowPath:
                                        if (inputController.arrowPath != null){
                                            player.shootArrow(inputController.arrowPath, wumpus);
                                            if (wumpus.isAlive == false)
                                                break;
                                            else
                                                inputController.mode = InputMode.getMove;
                                            }
                                            break;
            case InputMode.getPlayAgain: if(inputController.choice != false){
                                            if (inputController.input == "1"){
                                                setup(0);
                                                inputController.choice = false;
                                                inputController.mode = InputMode.getMove;
                                            }

                                            if(inputController.input == "2"){
                                                setup(1);
                                                inputController.choice = false;
                                                inputController.mode = InputMode.getMove;
                                            }

                                            if(inputController.input == "3")
                                                Application.Quit();
                                            }
                                            break;
            }
        }

    /*check to see if game is won or lost*/
    void updateGameState(){
        if (player.isAlive == false || player.arrowCount == 0) {
            STATE = 'L';
            inputController.mode = InputMode.getPlayAgain;
            }
        if (wumpus.isAlive == false){
            STATE = 'W';
            inputController.mode = InputMode.getPlayAgain;
            }
        return;
        }

    //public bool BAT, PIT, WUMPUS;
    /*handles events when player runs into hazards*/
    void handleHazards() {
        //bats carry off player
        foreach (Bat bat in bats){
            if (bat.location == player.location){
                //guiController.gameText = "Zap--Super Bat snatch! Elsewhereville for you!";
                bat.movePlayer(player, wumpusCave.rooms);
                }
            }

        //wumpus kills/wakes and runs
        if (wumpus.location == player.location){
            if (wumpus.isAwake == false){
                //guiController.gameText = "Oops, bumped a Wumpus!";
                wumpus.wake();
                wumpus.moveRandomly();
                }
            else{
                wumpus.kill(player);
                }
            }

        //pits kill player instantly
        foreach (Pit pit in pits){
            if (pit.location == player.location){
                //guiController.gameText = "YYYIIIIEEEE . . . fell in a pit";
                pit.kill(player);
                }
            }
        }

    /*sets up entities in proper postions*/
     public void setup(int setPos){
        if (STATE != 'u') {
            reset();
        }  //the game has been played before and everything needs to be reset

        if (setPos == 1)   //reset positions if needed
            setPositions();



        //place everything
        player.move(wumpusCave.rooms[positions[0]]);
        wumpus.move(wumpusCave.rooms[positions[1]]);
        bats[0].move(wumpusCave.rooms[positions[2]]);
        bats[1].move(wumpusCave.rooms[positions[3]]);
        pits[0].move(wumpusCave.rooms[positions[4]]);
        pits[1].move(wumpusCave.rooms[positions[5]]);
        }

    /*sets random positions for game entities*/
    public void setPositions(){
        System.Random rand = new System.Random();
        for (int i = 0; i < positions.Length; i++){
            positions[i] = rand.Next(rand.Next(0, wumpusCave.rooms.Count - 1));
            }
        }

    /*resets the state of wumpus and player*/
    public void reset(){
        STATE = 'u';

        System.Random rand = new System.Random();
        positions[0] = rand.Next(rand.Next(0, wumpusCave.rooms.Count - 1));

        wumpus.isAwake = false;
        wumpus.isAlive = true;
        wumpus.metPlayer = false;

        player.isAlive = true;
        player.initArrows();
        }
    }
