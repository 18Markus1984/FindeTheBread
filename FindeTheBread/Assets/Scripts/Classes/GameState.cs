using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public static class GameState 
{
    public static float killedEnemies;      
    public static Player player;                    //class with all important values for the next room for instance the weaponname, lifepoints, collected coins etc.  
    public static float nummberOfIterrations = 0;   //the numbber of rooms you survived 
    public static float difficulty;                 //value that you multiply with the spawnrate 
    public static float volume;
    public static float timer = 0;
    public static bool timerActive = false;
}
