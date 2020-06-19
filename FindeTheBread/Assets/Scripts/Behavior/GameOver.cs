using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public void GoBackMainMenu()    
    {
        Loader.Load(Loader.Scene.UI);   //open the new sceen UI
        GameState.timerActive = false;
    }
}
