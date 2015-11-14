using UnityEngine;
using System.Collections.Generic;

public class Room{
	public int id;
   	public List<Room> neighbors;
   	public int maxNeighbors = 3;
   	public List<IEntity> occupants;

	public Room(int id, int maxNeighbors){
		this.id = id;
        this.maxNeighbors = maxNeighbors;
        neighbors = new List<Room>(maxNeighbors);
        occupants = new List<IEntity>();
		}

	public void addNeighbor(Room room){
		if (hasNeighbor(room))	{return;}
		if (neighbors.Count < maxNeighbors){
			neighbors.Add(room);
			room.neighbors.Add(this);
			}
		}

	public bool hasNeighbor(Room neighbor){
	   if (neighbors.Contains(neighbor)){
		   return true;
	   	}
		return false;
   		}

	public void addOccupant(IEntity occupant){
	   	occupants.Add(occupant);
   		}

	 public void removeOccupant(IEntity occupant){
		 occupants.Remove(occupant);
	 	}

	 public bool hasOccupant(IEntity occupant){
		 if (occupants.Contains(occupant))
			 return true;
		 else
			 return false;
	 	}

	 public bool hasOccupantType(EntityType type){
		 foreach(IEntity occupant in occupants){
			 if (occupant.Type == type)
				 return true;
		 	}
		 return false;
	 		}

	 public IEntity getOccupant(EntityType type){
		 foreach (IEntity entity in occupants){
			 if (entity.Type == type)
				 return entity;
		 	}
		 return null;
	 	}
	}
