using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

    /*  A class that represents the Wumpus Cave.
     *  20 rooms.
     *  Each room must be connected to 3 other rooms.
     *  There must be a way to reach each room.
     *  No room may be unreachable.
     */

public class Cave{
    public List<Room> rooms;
    public List<IEntity> occupants;

    /*Cave with max Rooms*/
    public Cave(){
        rooms = CreateRoomsList();
        int[,] connections = CreateConnectionArray();

        for (int i = 0; i < connections.GetLength(0); i++) {//for each room
            for (int j = 0; j < connections.GetLength(1); j++)  {//add each connection
                connectRooms(this.rooms[i], this.rooms[connections[i,j]-1]);
                }
            }
        }

    /*Fully connected Cave*/
    public Cave(List<Room> rooms, int[,] connections){
        this.rooms = new List<Room>(rooms.Count);

        }

    public void addOccupant(IEntity occupant, Room room){
        occupants.Add(occupant);
        room.addOccupant(occupant);
        }

    public void addRoom(Room room){
        rooms.Add(room);
        }

    public void connectRooms(Room rm1, Room rm2){
        rm1.addNeighbor(rm2);   //Room class auto reciprocates neighbor-adding (rm2.addNeighbor(rm1);
        }

    public void printCaveLayout(){
        UnityEngine.Debug.Log("*****Cave Layout*****");
        foreach (Room room in rooms){
            UnityEngine.Debug.Log("Room id : " + room.id + " | ");
            foreach (Room neighbor in room.neighbors){
                UnityEngine.Debug.Log(neighbor.id + " ");
                }
            }
        }

    public List<Room> CreateRoomsList(){
        List <Room>somerooms = new List<Room>(20);
        for (int i = 0; i < somerooms.Capacity; i++)
        {
            Room room = new Room(i, 3);
            somerooms.Add(room);
        }
        return somerooms;
        }

    public int [,] CreateConnectionArray(){
        int [,] connections = new int[,]
        {
            {2, 5, 8},      //Room 1
            {1, 3, 10},     //Room 2
            {2, 4, 12},     //Room 3
            {3, 5, 14},     //Room 4
            {1, 4, 6},      //Room 5
            {5, 15, 7},     //Room 6
            {6, 8, 17},     //Room 7
            {1, 7, 11},     //Room 8
            {12, 10, 19},   //Room 9
            {2, 9, 11},     //Room 10
            {8, 20, 10},    //Room 11
            {3, 13, 9},     //Room 12
            {12, 14, 18},   //Room 13
            {4, 13, 15},    //Room 14
            {6, 16, 14},    //Room 15
            {15, 18, 17},   //Room 16
            {7, 16, 20},    //Room 17
            {13, 16, 19},   //Room 18
            {18, 9, 20},    //Room 19
            {19, 17, 11}    //Room 20
        };
        return connections;
        }

    }
