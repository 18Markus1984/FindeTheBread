using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
   
    public void PlayGame()
    {
        GameState.difficulty = 1;
        GameState.killedEnemies = 0;
        GameState.nummberOfIterrations = 0;
        GameState.timer = 0.0f;
        string weapon = GameState.player.nameWeapon;
        string bullet = GameState.player.bulletName;
        float shooting = GameState.player.weaponShootingTime;
        float damage = GameState.player.damage;

        GameState.player = new Player(6, 6, 0, 0, shooting , damage, weapon, bullet);
        Loader.Load(Loader.Scene.Gameplay);     //open new Sceen Gameplay
    }

    public void QuitGame()
    {
        Application.Quit();         //close the program
    }

    public void PlayStory()
    {
        Loader.Load(Loader.Scene.StartGame);        //open the new Sceen StartGame
    }

    public void SetVolume(float volume)        //call the function when the options menu opens
    {
        AudioListener.volume = volume;
        GameState.volume = volume;
    }

    public void ActivitedTimer()
    {
        GameState.timerActive = true;
        GameState.timer = 0;
    }
}
