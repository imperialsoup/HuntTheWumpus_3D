using System;
using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;

public class Player : IEntity
{
    public  string      name = "";
    public  Room        location;
    public  bool        isAlive;
    public  int         arrowCount;
    public List<CrookedArrow> arrows;
    public  const int   arrowMax = 5;
    public string warningMessage;

    private  EntityType  type;
    public EntityType Type{
		get	{	return type;	}
		set	{	type = value;	}
    	}

    public Player(string name){
        this.name = name;
        this.Type = EntityType.player;
		isAlive = true;
        arrowCount = arrowMax;
        arrows = new List<CrookedArrow>(arrowMax);
        initArrows();
        UnityEngine.Debug.Log("Player " + name + " is born.");
    	}

    public Player(string name, Room room){
        this.name = name;
        this.Type = EntityType.player;
		isAlive = true;
        initArrows();
        move(location);
    	}

    public void initArrows() {
        arrowCount = arrowMax;
        arrows = new List<CrookedArrow>(arrowMax);

        for (int i = 1; i <= arrows.Capacity; i++){
            CrookedArrow arrow = new CrookedArrow();
            arrows.Add(arrow);
        	}
    	}

    public void move(Room room){
        if (location == null){
            location = room;
            location.addOccupant(this);
        	}
        else {
            location.removeOccupant(this);  //leave the current room
            location = room;
            location.addOccupant(this);         //enter the new Room.
        	}
        UnityEngine.Debug.Log("Player is now in Room " + (location.id + 1));
        this.sense();
    	}

    public void die(){
        isAlive = false;
        //Debug.Write("Player: Oh no! " + name + " is daed! " );
    	}

    public void sense(){
        warningMessage = "";
        foreach (Room room in location.neighbors){
            if (room.hasOccupantType(EntityType.wumpus))
                warningMessage += "I smell a Wumpus...\n";
            if (room.hasOccupantType(EntityType.pit))
                warningMessage += "I feel a draft...\n";
            if (room.hasOccupantType(EntityType.bat))
                warningMessage += "I hear bat wings...\n";
    		}
    	}

    
    public void shootArrow(List<Room> path, Wumpus target) {
        if (arrowCount > 0) {
            arrows[0].fly(path, location, target);
            arrows.Remove(arrows[0]);
            arrowCount--;
            Console.WriteLine(name + " has shot a crooked arrow. " + arrowCount + " crooked arrows are left.");
	        }
        if (arrowCount <= 0)
            Console.WriteLine("Out of arrows!");
    	}
	}
