using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="LevelCreationData.asset", menuName ="LevelCreationData/Level Data")]
public class LevelCreationData : ScriptableObject {     //data for the map generation

    public int numberOfWalkers;
    public int numberOfIterations;

    public int WidthInSprites;
    public int HeightInSprites;

    public int forbbidenAreaWidth;  //line the walkers aren't alowed to cross
    public int forbbiddenAreaHeight;

    public int tileSize;            //size of the area the walkers fill

    public LevelCreationData(int numberOfWalkers, int numberOfIterations, int widthInSprites, int heightInSprites, int forbbidenAreaWidth, int forbbiddenAreaHeight, int tileSize)
    {
        this.numberOfWalkers = numberOfWalkers;
        this.numberOfIterations = numberOfIterations;
        WidthInSprites = widthInSprites;
        HeightInSprites = heightInSprites;
        this.forbbidenAreaWidth = forbbidenAreaWidth;
        this.forbbiddenAreaHeight = forbbiddenAreaHeight;
        this.tileSize = tileSize;
    }
}
