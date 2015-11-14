using UnityEngine;
using System;
using System.Collections.Generic;

public class Pit : IEntity{
    public Room location;
    int id;

    public Pit(int id){
        this.id = id;
        this.Type = EntityType.pit;
        location = null;
        UnityEngine.Debug.Log("Pit " + id + " is born.");
        }
    public Pit(int id, Room room)
    {
        this.id = id;
        this.Type = EntityType.pit;
        location = room;
        location.addOccupant(this);
        UnityEngine.Debug.Log("Pit " + id + " is born in Room " + (location.id + 1));
    }

    private EntityType type;

    public EntityType Type
    {
        get
        {
            return type;
        }

        set
        {
            type = value;
        }
    }

    public void kill(Player player)
    {
        player.die();
    }
    public void move(Room room)
    {
        if (location == null)
        {
            location = room;
            location.addOccupant(this);
        }
        else
        {
            location.removeOccupant(this);  //leave the current room
            location = room;
            location.addOccupant(this);         //enter the new Room.
        }
        UnityEngine.Debug.Log("Pit " + id + " is now in room " + (location.id + 1));
    }

}
