using UnityEngine;
using System.Collections.Generic;

public class CrookedArrow : IEntity{
        public Room location;
        public bool used;
        public const int maxFlight = 5;
        public bool hitTarget = false;
        private EntityType type;

        public EntityType Type{
            get	{	return type;	}
			set	{	type = value;	}
        	}

        public CrookedArrow(){
            used = false;
            this.Type = EntityType.arrow;
        	}

        public void fly(List<Room> path, Room current, Wumpus target){
            //Random rand = new Random();
            location = current;
            location.addOccupant(this);
            int flightLength = 0;

            //TODO: this is ugly, clean up later
            foreach (Room room in path){   //check current length of flight
                if (flightLength < maxFlight){
                    if (location.hasOccupant(target)){
                        target.die();
                        //Console.WriteLine("Aha! You got the Wumpus!");
                        return;
                    	}
                    //check if rooms are connected
                    if (location.hasNeighbor(room)){
                        move(room);
                    	}
                    else{
                        //Console.WriteLine("Room " + (location.id + 1) + " is not neighbors with Room " +
												//	(room.id + 1) + "! Picking another room...");
                        int i = Random.Range(0, location.neighbors.Count - 1);
                        move(location.neighbors[i]);
                    	}
                    if (location == current){
                        //Console.WriteLine("Ouch! The arrow got you!");
                        break;
                    	}
                    flightLength++;
                	}
            }
            //Console.WriteLine("Missed!");
            target.wake();  //wake the wumpus if you miss
            used = true;
    		}

        public void move(Room room){
            location.removeOccupant(this);  //leave the current room
            location = room;
            location.addOccupant(this);         //enter the new Room.
            //Debug.WriteLine("Arrow.move(): The arrow is in Room " + (location.id + 1));
            //Console.WriteLine("The arrow is in Room " + (location.id + 1));
    		}
    }
