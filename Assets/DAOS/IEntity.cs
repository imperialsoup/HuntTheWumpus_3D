using UnityEngine;
using System.Collections;

public enum EntityType{
       wumpus,
       player,
       bat,
       pit,
       arrow
   }

   public interface IEntity{
       EntityType Type
       {
           get;
           set;
       }

       void move (Room room);
   }
