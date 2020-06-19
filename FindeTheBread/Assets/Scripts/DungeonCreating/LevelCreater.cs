using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class LevelCreater : MonoBehaviour {

    [HideInInspector]
    public LevelCreationData levelCreationData;     //data to create the random level
    public Tilemap tilemapToDrawFloor;              //bottom Tilemap
    public Tilemap tilemapToDrawHigherFloor;        //Top Tilemap
    public Tilemap tilemapDecoration;               //DecorationTop Tilemap
    public Tilemap tilemapbottomDecoration;         //DecorationBottom Tilemap

    public TileBase fillRuleTile;                   //regular RuleTileset
    public TileBase decoration;                     //decorationTop RuleTileset
    public TileBase bottomDecoration;               //decorationBottom RuleTileset

    public Tile topTile;                            //Tile for Top Tilemap
    public Tile hole;                               //Levelenterplace (hole in the ground where the player enters the level)
    public Tile testTile;                           //test Tile for testing

    private HashSet<Vector2Int> dungeonTiles;       

    public float spawnRate = 1f;                    //rate for spawning enemies
    public List<GameObject> enemies;                //all enemies 

    public GameObject spike;                        
    public GameObject doorForNextLevel;             
    public GameObject tree;
    public GameObject loak;
    public GameObject patrolPosition;               //gameobject to generate random points for random patrol

    [HideInInspector]
    public GameObject player;                       //player

    private Vector2Int[] posiblePositionDoor = new Vector2Int[100];        //on the right side
    private Vector2Int[] posiblePositionDoorNegative = new Vector2Int[100]; //on the left side
    private List<Vector2Int> vector2Ints = new List<Vector2Int>();          //allBottomTiles    
    private List<Vector2Int> treePositions = new List<Vector2Int>();        //allTopTiles

    

    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");    
        levelCreationData = LevelDataCreater.CreateLevelData();     
    }

    public void Start()
    {
        if (GameState.nummberOfIterrations > 0)     //if this round isn't the first round in a room 
        {                                           //set the stats from the room before to the stats 
            
                player.GetComponent<PickUpItems>().lifepoints = GameState.player.lifepoints;
                player.GetComponent<PickUpItems>().maxLifepoints = GameState.player.maxLifepoints;
                player.GetComponent<PickUpItems>().keys = GameState.player.keys;
                player.GetComponent<PickUpItems>().money = GameState.player.money;
                spawnRate = GameState.difficulty;
                Debug.Log(spawnRate);
                foreach (GameObject weapon in GameObject.FindGameObjectsWithTag("Weapon"))
                {
                    if (GameState.player.nameWeapon == weapon.name)
                    {
                        weapon.GetComponent<Weapon>().pickedUp = true;
                        player.GetComponent<PickUpItems>().weapon = weapon;
                    }
                }
        }

        dungeonTiles = DrunkenWalkerManager.CreateMap(levelCreationData, CreateZone(levelCreationData.forbbiddenAreaHeight,levelCreationData.forbbidenAreaWidth));  //starts the drunkenWalker
        if(levelCreationData.tileSize > 1)
        {
            dungeonTiles = ReturnListOfScaledTiles(dungeonTiles); //scale the tiles 
        }
        DrawDungeonTiles(dungeonTiles, tilemapToDrawFloor, fillRuleTile);   //starts to draw the tiles
        CreateTrees();  //create random decoration on the top Tilemap
    }

    private void DrawDungeonTiles(IEnumerable<Vector2Int> tiles, Tilemap tilemapToUse, TileBase tilesToUse)
    {
        foreach (Vector2Int tileLocation in tiles)  //draw all dungeon tilles at the positions we get from the drunkenWalker functions
        {
            tilemapToUse.SetTile(new Vector3Int(tileLocation.x, tileLocation.y, 0), tilesToUse);
        }

        foreach (Vector2Int v in tiles) //transforms the IEnurable<Vector2Int> into a List<Vector2Int>
        {
            vector2Ints.Add(v);
        }

        Vector2Int position;
        do      //set the postion to a new position until it is a position which has the postion.x % != 0
        {
            position = vector2Ints[FindePositionTouchingPerspektive()];
        }
        while (position.x % 2 == 0);
        GameObject door = Instantiate(doorForNextLevel, new Vector3(position.x, position.y + (float)0.61), new Quaternion(0, 0, 0, 0));     //create the door at the position we get from the function

        position = vector2Ints[FindePositionNotTouchingPerspektive()];      //sets position to a new Vector2Int on the bottomTilemap 
        tilemapDecoration.SetTile(new Vector3Int(position.x, position.y, 0), hole);     //set the entering hole 
        player.GetComponent<Transform>().position = new Vector3(position.x + 0.5f, position.y + 1f, 0); //set the player startposition to the entering door position

        for (int i = 0; i < 3; i++)     //finde three positions for spikes 
        {
            position = vector2Ints[FindePositionNotTouchingPerspektive()];  //sets position to a new Vector2Int on the bottomTilemap 
            Instantiate(spike, new Vector3(position.x + 0.5f, position.y, 0), new Quaternion(0, 0, 0, 0));  //create a new object clone of spike
        }

        List<GameObject> activeEnemys = new List<GameObject>();
        if (spawnRate == 1)             //if spawnrate is smaller than 2.5 spawn shooter and smallEnemy and the other case also the charger
        {
            activeEnemys.Add(enemies[0]);
        }
        else if (spawnRate <= 2.5f)
        {
            activeEnemys.Add(enemies[0]);
            activeEnemys.Add(enemies[1]);
        }
        else if (spawnRate >= 2.5f)
        {
            activeEnemys.Add(enemies[0]);
            activeEnemys.Add(enemies[1]);
            activeEnemys.Add(enemies[2]);
        }
        for (int i = 0; i < 2.2f * spawnRate; i++)          //spawn as many enemies as spawnrate multiplite bz 2.2
        {
            position = vector2Ints[FindePositionNotTouchingPerspektive()];
            GameObject enemy = Instantiate(activeEnemys[Random.Range(0, activeEnemys.Count)], new Vector3(position.x - 0.5f, position.y + 0.5f), new Quaternion(0, 0, 0, 0));   //create a new random enemy by using the list we created before
        }

        Vector2Int[] vorherigepositionen = new Vector2Int[3];   
        for (int i = 0; i < 110; i++)   //create random decoration on the Tilemap bottom decoration
        {
            if (Random.Range(1, 10) == 1)
            {
                position = vector2Ints[FindePositionNotTouchingPerspektive()];
                tilemapbottomDecoration.SetTile(new Vector3Int(position.x, position.y, 0), bottomDecoration);
            }
        }
        DrawTopTiles(tilemapToDrawFloor, tilemapToDrawHigherFloor);     //draw the Top Tiles on the topFloor
    }

    public Transform[] CreatePatrolPositionObjects()    //gives back a array with random position on the map the enemies walk between
    {
        Transform[] transforms = new Transform[30];
        Vector2Int position;
        for (int i = 0; i < 30; i++)
        {
            position = vector2Ints[FindePositionNotTouchingPerspektive()];
            GameObject patrolPoint = Instantiate(patrolPosition, new Vector3(position.x, position.y, 0), new Quaternion(0, 0, 0, 0));
            transforms[i] = patrolPoint.GetComponent<Transform>();      //set transforms at postio i to the position of the new instantiated obcjet 
        }
        return transforms;
    }

    private int FindePositionNotTouchingPerspektive()       //returns a random position from the bottomPositions that doesn't hit the perspektive tiles 
    {
        int random = 0;
        bool checker = false;
        do
        {
            checker = false;
            random = Random.Range(0, vector2Ints.Count);
            Vector2Int item = vector2Ints[random];
            if (tilemapToDrawFloor.GetTile(new Vector3Int(item.x, item.y, 0)) != null && tilemapToDrawFloor.GetTile(new Vector3Int(item.x, item.y + 1, 0)) == null)     //if bottomtilemap isn't null at vector2Int[position] and check that about this one is null; if this conditions fit the checker is set to true and the loop have to find a new position
            {
                checker = true;
            }
        } while (vector2Ints[random].x > 2000 || checker);

        return random;
    }

    private int FindePositionTouchingPerspektive()       //returns a random position from the bottomPositions is one of the perspektive tiles 
    {
        int random = 0;
        bool checker = true;
        do
        {
            checker = true;
            random = Random.Range(0, vector2Ints.Count);
            Vector2Int item = vector2Ints[random];
            if (tilemapToDrawFloor.GetTile(new Vector3Int(item.x, item.y, 0)) != null && tilemapToDrawFloor.GetTile(new Vector3Int(item.x, item.y + 1, 0)) == null)     //if bottomtilemap isn't null at vector2Int[position] and check that about this one is null; if this conditions fit the checker set this as a possible position
            {
                checker = false;
            }
        } while (vector2Ints[random].x > 2000 || checker);

        return random;
    }

    private HashSet<Vector2Int> ReturnListOfScaledTiles(IEnumerable<Vector2Int> tiles)      //Scales the walked way to the right tilesize
    {
        HashSet<Vector2Int> scaledTiles = new HashSet<Vector2Int>();

        foreach (Vector2Int tileLocation in tiles)  //every tile will be scaled
        {
            Vector2Int startPosition = tileLocation * levelCreationData.tileSize;
            Vector2Int newPosition;

            for (int i = 0; i < levelCreationData.tileSize; i++)    //scale the tiles up with the tilesize factor you have set in the levelcreationdata
            {
                for (int j = 0; j < levelCreationData.tileSize; j++)
                {
                    newPosition = new Vector2Int(i, j) + new Vector2Int(startPosition.x, startPosition.y);
                    scaledTiles.Add(newPosition);
                }
            }
        }
        return scaledTiles;
    }

    public Vector2Int FindeNewPositionfortheTree()  //if the tree hits the bottomtilemap or another tree it executes this function to find a new location whitout hitting another collider
    {
        return treePositions[Random.Range(0, treePositions.Count)];
    }

    private void CreateTrees()      //create tree and loak decoration 
    {
        int y = levelCreationData.HeightInSprites;
        int x = levelCreationData.WidthInSprites;

        for (int i = -(x / 2); i < x / 2 + 1; i++)      //find the position that are the higher Floor 
        {
            for (int j = -(y / 2); j < y / 2 + 1; j++)
            {
                if (tilemapToDrawHigherFloor.GetSprite(new Vector3Int(i, j, 0)) != null)
                {
                    treePositions.Add(new Vector2Int(i, j));
                }
            }
        }
        for (int i = 0; i < 5; i++)     //spawn 5 trees and 1 loak
        {
            int position = Random.Range(0, treePositions.Count);
            Instantiate(tree, new Vector3(treePositions[position].x, treePositions[position].y), new Quaternion(0, 0, 0, 0));
            if (i == 4)
            {
                position = Random.Range(0, treePositions.Count);
                Instantiate(loak, new Vector3(treePositions[position].x, treePositions[position].y), new Quaternion(0, 0, 0, 0));
            }
        }
    }

    private void DrawTopTiles(Tilemap tilemapToCheck, Tilemap tilemapToDraw)
    {
        int y = levelCreationData.HeightInSprites;
        int x = levelCreationData.WidthInSprites;

        for (int i = -(x/2); i < x/2+1; i++)        //check which sprite in the bottom tilemap isn't given 
        {
            for (int j = -(y/2); j < y/2+1; j++)
            {
                if (tilemapToCheck.GetSprite(new Vector3Int(i, j, 0)) == null)
                {
                    tilemapToDraw.SetTile(new Vector3Int(i, j, 0), topTile);    //draw at the position where in the bottom tilemap no tile is given
                    if (Random.Range(0,10) == 1)        //with the factor of 1/10 create a decoration layer for the topfloor
                    {
                        tilemapDecoration.SetTile(new Vector3Int(i, j, 0), decoration);
                    }
                }
            }
        }
    }

    private List<Vector2Int> CreateZone(int x, int y)       //create a border that the walkers shoulden't cross 
    {

        List<Vector2Int> forbbidenVectors = new List<Vector2Int>();

        for (int i = -(x / 2); i < x / 2; i++)
        {
            forbbidenVectors.Add(new Vector2Int(i,-(y/2)));
            forbbidenVectors.Add(new Vector2Int(i, y / 2));
        }

        for (int i = -(y / 2); i < y / 2; i++)
        {
            forbbidenVectors.Add(new Vector2Int(-(x / 2), i));
            forbbidenVectors.Add(new Vector2Int(x / 2, i));
        }

        return forbbidenVectors;    //give back the position of the rectangle
    }
}
