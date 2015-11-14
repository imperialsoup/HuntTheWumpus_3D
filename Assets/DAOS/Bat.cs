using UnityEngine;
using System;
using System.Collections.Generic;

public class Bat : IEntity{
    public string name = "";
    public Room location;

    public Bat(string name)
    {
        //isAlive = true;
        this.name = name;
        this.Type = EntityType.bat;
        location = null;
        UnityEngine.Debug.Log("Bat " + name + " is born .");
    }
    //TODO: Change the bat name to int id...
    public Bat(string name, Room room){
        //isAlive = true;
        this.name = name;
        this.Type = EntityType.bat;
        location = room;
        location.addOccupant(this);
        UnityEngine.Debug.Log("Bat " + name + " is born in Room " + (location.id + 1));
        }

    private EntityType type;

    public EntityType Type{
        get {    return type;   }

        set {    type = value;   }
        }

    public void movePlayer(Player player, List<Room> rooms){
        System.Random rand = new System.Random();
        int i = rand.Next(0, rooms.Count - 1);

        //Console.WriteLine("Bat.movePlayer() : " + name + " is carrying " + player.name + " to Room " + rooms[i].id);
        player.move(rooms[i]);
        UnityEngine.Debug.Log("You watch the superbat fly away into the darkness...");
        //i = rand.Next(0, rooms.Count - 1); // make the bat move somewhere new as well
        //move(rooms[i]);
    }

    public void move(Room room){
        if (location == null){
            location = room;
            location.addOccupant(this);
            }
        else{
            location.removeOccupant(this);  //leave the current room
            location = room;
            location.addOccupant(this);         //enter the new Room.
            }

        UnityEngine.Debug.Log("Bat " + name + " is now in room " + (location.id + 1));
        }

}
