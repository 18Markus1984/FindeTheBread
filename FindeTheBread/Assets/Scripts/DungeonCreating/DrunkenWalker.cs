using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrunkWalker {

    public Vector2Int position { get; set;}
	
    public DrunkWalker(Vector2Int startPosition)
    {
        position = startPosition; //set position to starposition
    }

    public Vector2Int Move(Dictionary<Direction,Vector2Int> directionMovementMapping, List<Vector2Int> forbbidenVectors)
    {
        
        do
        {
            Direction toMove = (Direction)Random.Range(0, directionMovementMapping.Count);  //set a random direction to move
            position += directionMovementMapping[toMove];       //add the random direction to the position 

            foreach (Vector2Int forbbidenVector in forbbidenVectors)    //checks if the position isn't in the area of the forbiddenVectors
            {
                if (position == forbbidenVector)
                {
                    position = new Vector2Int(2000, 2000);
                }
            }

        } while (position == new Vector2Int(2000, 2000));   //koregieren!!!!!!

        return position;    //give the position back
    }
}
