using UnityEngine;
using System;
using System.Diagnostics;
using System.Collections.Generic;

    public class Wumpus : IEntity
    {
        public string name = "";
        public Room location;
        public bool isAwake = false;
        public bool isAlive;
        public bool metPlayer;
        System.Random dice;

        public Wumpus(string name)
        {
            isAlive = true;
            this.name = name;
            this.Type = EntityType.wumpus;
            metPlayer = false;
            dice = new System.Random();
            location = null;
            System.Diagnostics.Debug.WriteLine("Wumpus is born.");
        }
        public Wumpus(string name, Room room)
        {
            isAlive = true;
            this.name = name;
            this.Type = EntityType.wumpus;
            metPlayer = false;
            location = room;
            location.addOccupant(this);
            dice = new System.Random();
            System.Diagnostics.Debug.WriteLine("Wumpus is born in Room " + (location.id + 1));
        }

        private EntityType type;
        public EntityType Type{
            get {   return type;    }
            set {   type = value;   }
            }

        public void wake(){
            isAwake = true;
            System.Diagnostics.Debug.WriteLine("Wumpus.wake(): The Wumpus wakes!");
            }

        public void move(Room room){
            if (location == null){
                location = room;
                location.addOccupant(this);
                }
            else    {
                location.removeOccupant(this);  //leave the current room
                location = room;
                location.addOccupant(this);         //enter the new Room.
                }
            System.Diagnostics.Debug.WriteLine("Wumpus.move() : " + name + " has moved to Room " + (location.id + 1));
        }

        public void die()   {
            isAlive = false;
            System.Diagnostics.Debug.WriteLine("Wumpus.die(): The Wumpus is dead.");
        }

        public void kill(Player player)
        {
            player.die();
            System.Diagnostics.Debug.WriteLine("Ooh! The Wumpus got ya!");
        }

        public void diceRoll()
        {
            int roll = dice.Next(1, 100);

            System.Diagnostics.Debug.WriteLine("You rolled a " + roll);
            if (roll <= 25)
            {
                System.Diagnostics.Debug.WriteLine("The Wumpus decided to stay put.");
            }
            if (roll > 25)
            {
                moveRandomly();
            }
        }

        public void moveRandomly()
        {
            int roll = dice.Next(0, location.neighbors.Count - 1); // move into a random adjacen room
            move(location.neighbors[roll]);
        }
    }
