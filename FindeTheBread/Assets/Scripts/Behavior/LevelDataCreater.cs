using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDataCreater: MonoBehaviour
{
    public static LevelCreationData CreateLevelData()
    {
        return new LevelCreationData(7, 30, 31, 26, 12, 15, 2);     //the values for the random Level creation that the drunkenWalker work as I want
    }
}
