using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    North = 0,
    East  = 1,
    South = 2,
    West  = 3
};

public class DrunkenWalkerManager : MonoBehaviour
{
    private static readonly Dictionary<Direction, Vector2Int> directionMovemntMapping = new Dictionary<Direction, Vector2Int>
    {
        {Direction.North, Vector2Int.up },
        {Direction.South, Vector2Int.down },
        {Direction.East, Vector2Int.right },
        {Direction.West, Vector2Int.left },
    };

    public static HashSet<Vector2Int> CreateMap(LevelCreationData levelData, List<Vector2Int> forbbidenVectors)
    {
        List<DrunkWalker> drunkWalkers = new List<DrunkWalker>();   //list with all drunkenWalker objects
        HashSet<Vector2Int> positionsVisited = new HashSet<Vector2Int>();   //list with all positions the drunkenwalker walked

        for (int i = 0; i < levelData.numberOfWalkers; i++)     //create the drunken walkers
        {
            drunkWalkers.Add(new DrunkWalker(Vector2Int.zero));
        }

        for (int i = 0; i < levelData.numberOfIterations; i++)  //let the walkers walk
        {
            foreach  (DrunkWalker dr in drunkWalkers)
            {
                Vector2Int newPosition = dr.Move(directionMovemntMapping, forbbidenVectors);    //create 
                positionsVisited.Add(newPosition);      //add position to the position list
            }
        }
        return positionsVisited;
    }
}
