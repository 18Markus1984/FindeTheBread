using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinStats : MonoBehaviour
{
    public Image imageWeapon;                   //last pickedUp weapon
    public List<TMPro.TextMeshProUGUI> stats;       //gamestate text 
    public List<Sprite> sprites;                //weapon sprites

    public TMPro.TextMeshProUGUI lastSentence;

    void Start()
    {
        stats[0].text = "" + GameState.nummberOfIterrations; //rooms finished
        stats[1].text = "" + GameState.player.money;           //coins collected
        stats[2].text = GameState.player.lifepoints +"/"+GameState.player.maxLifepoints; // lifepoints/maxLifepoints
        stats[3].text = "" + GameState.killedEnemies;       
        stats[4].text = GameState.player.nameWeapon;
        stats[5].text = "" + GameState.player.weaponShootingTime; //fireRate
        stats[6].text = "" + GameState.player.damage;

        string bulletName = GameState.player.bulletName;
        if (bulletName == "Beam" || bulletName == "Bullet")
        {
            imageWeapon.sprite = sprites[0];
            if (bulletName == "Beam")
            {
                imageWeapon.sprite = sprites[1];
            }
        }
        else if (bulletName == "Spark")
        {
            imageWeapon.sprite = sprites[2];
        }

        lastSentence.text = "You made it. You find the legendary bread. You killed " + GameState.killedEnemies + //shows a last sentence with all stats
            " enemies in " + GameState.nummberOfIterrations + " Rooms. You have " + GameState.player.lifepoints + " lifepoints left after you had killed " + GameState.killedEnemies + " enemies. ";

        if (GameState.timerActive)
        {
            lastSentence.text = "You made it. You find the legendary bread. You killed " + GameState.killedEnemies + //shows a last sentence with all stats
            " enemies in " + GameState.nummberOfIterrations + " Rooms. You have " + GameState.player.lifepoints + " lifepoints left after you had killed " + GameState.killedEnemies + " enemies. " +
            "You needed for this " + Mathf.Floor((GameState.timer % 3600) / 60).ToString("00") + " minutes, " + (GameState.timer % 60).ToString("00") + " seconds" +
            " and " + (Mathf.Floor(float.Parse(((GameState.timer % 1).ToString()).Substring(0, 6)) * 10000)).ToString("0000") + " miliseconds. Thank You for playing!";
        }
        
    }
}
