using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject enemies;          //folder with all enemy referenz objects
    public Image imageWeapon;           //the image at the gameState that 
    public Canvas pauseScreen;          
    public Canvas gameOver;             //gameOver sccreen
    public List<TMPro.TextMeshProUGUI> stats;       //list with all gamestat's text elements
    public List<Sprite> sprites;                    //weapon sprites
    private GameObject player;
    public Timer timer;

    public float endCondition;          //value wwhen reached with the spawnrate end the gameplay and open the end sceen 

    private bool checker = false;           //cheecks if the pauseScreen is opened

    private void Start()
    {
        enemies.SetActive(false);                               //hides the gameobject enemies
        AudioListener.volume = GameState.volume;
        player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(GameState.timerActive);
        if (GameState.timerActive && SceneManager.GetActiveScene().name != "StartGame")
        {
            if (GameState.timer == 0 && GameState.nummberOfIterrations > 1)
            {
                timer.playing = true;
            }
            else
            {
                timer.theTime += GameState.timer;
            }
        }
        else if (!GameState.timerActive && SceneManager.GetActiveScene().name != "StartGame")
        {
            GameObject timer = GameObject.FindGameObjectWithTag("Timer");
            timer.SetActive(false);
        }
        
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "StartGame")      //if the active scene isn't StartGame set the stats 
        {
            string bulletName = "Bug";
            stats[0].text = "" + GameState.nummberOfIterrations; //rooms finished
            stats[1].text = "" + GameObject.FindGameObjectWithTag("Player").GetComponent<PickUpItems>().money;      //coins you collected
            stats[2].text = player.GetComponent<PickUpItems>().lifepoints + "/" + player.GetComponent<PickUpItems>().maxLifepoints;     // lifepoints/Maxlifepoints
            stats[3].text = "" + GameState.killedEnemies;   //killed Enemies
            if (player.GetComponent<PickUpItems>().weapon == null)
            {
                player.GetComponent<PickUpItems>().lifepoints = GameState.player.lifepoints;
                player.GetComponent<PickUpItems>().maxLifepoints = GameState.player.maxLifepoints;
                player.GetComponent<PickUpItems>().keys = GameState.player.keys;
                player.GetComponent<PickUpItems>().money = GameState.player.money;
                foreach (GameObject weapon in GameObject.FindGameObjectsWithTag("Weapon"))
                {
                    if (GameState.player.nameWeapon == weapon.name)
                    {
                        weapon.GetComponent<Weapon>().pickedUp = true;
                        player.GetComponent<PickUpItems>().weapon = weapon;
                    }
                }
            }

            stats[4].text = player.GetComponent<PickUpItems>().weapon.name;     //set weapon name
            stats[5].text = "" + player.GetComponent<PickUpItems>().weapon.GetComponent<Weapon>().shootingTime;     //set fireRate
            bulletName = player.GetComponent<PickUpItems>().weapon.GetComponent<Weapon>().configertBullet.name;  //bulletName

            if (bulletName == "Beam" || bulletName == "Bullet")
            {
                stats[6].text = "" + GameObject.FindGameObjectWithTag("Player").GetComponent<PickUpItems>().weapon.GetComponent<Weapon>().configertBullet.GetComponent<Bullet>().damage; //damage
                imageWeapon.sprite = sprites[0];    //the weapon image
                if (bulletName == "Beam")
                {
                    imageWeapon.sprite = sprites[1];    //the weapon image2
                }
            }
            else if (bulletName == "Spark")
            {
                stats[6].text = "" + GameObject.FindGameObjectWithTag("Player").GetComponent<PickUpItems>().weapon.GetComponent<Weapon>().configertBullet.GetComponent<BulletSpark>().damage;
                imageWeapon.sprite = sprites[2];    //the weapon image3
            }
        }
        else if (SceneManager.GetActiveScene().name == "EndGame")    //if the active sceen is Endgame but here we can use the gamestate values because the values don't change
        {
            stats[0].text = "" + GameState.nummberOfIterrations;
            stats[1].text = "" + GameState.player.money;
            stats[2].text = GameState.player.lifepoints + "/" + GameState.player.maxLifepoints;
            stats[3].text = "" + GameState.killedEnemies;
            stats[4].text = GameState.player.nameWeapon;
            stats[5].text = "" + GameState.player.weaponShootingTime;
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
        }
    }

    public void enableCanavas()
    {
        Time.timeScale = 1.0f;
        checker = false;                                 //pauseScreen is inactive
        pauseScreen.gameObject.SetActive(false);
    }
    
    public void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))       //if the user press esc
        {
            if (!checker)
            {
                if (SceneManager.GetActiveScene().name != "EndGame")
                {
                    Time.timeScale = 0;                 //timefactor for movment to 0 so every script stops
                }
                pauseScreen.gameObject.SetActive(true);         //set pausescreen active
                checker = true;                                 //the pausescreen is active
            }
            else
            {
                Time.timeScale = 1.0f;
                checker = false;                                 //pauseScreen is inactive
                pauseScreen.gameObject.SetActive(false);
            }
            
        }

        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PickUpItems>().lifepoints <= 0)     //if player has no lifepoints
        {
            if (SceneManager.GetActiveScene().name != "EndGame")
            {
                Time.timeScale = 0;                 //timefactor for movment to 0 so every script stops
            }
            checker = true;
            gameOver.gameObject.SetActive(true);            //actived the gameOver Screen
        }
    }

    public void StartNextLevel()    //executed when player killed all enmies & stand at the door object 
    {
        string bulletName = GameObject.FindGameObjectWithTag("Player").GetComponent<PickUpItems>().weapon.GetComponent<Weapon>().configertBullet.name;      //set active bullet name
        GameObject player = GameObject.FindGameObjectWithTag("Player");         //set player gameobject


        if (SceneManager.GetActiveScene().name == "Gameplay")
        {
            GameState.timer = timer.theTime;
        }

        if (bulletName == "Beam" || bulletName == "Bullet")
        {
            GameState.player = new Player(player.GetComponent<PickUpItems>().lifepoints, player.GetComponent<PickUpItems>().maxLifepoints, player.GetComponent<PickUpItems>().money, player.GetComponent<PickUpItems>().keys, GameObject.FindGameObjectWithTag("Player").GetComponent<PickUpItems>().weapon.GetComponent<Weapon>().shootingTime, GameObject.FindGameObjectWithTag("Player").GetComponent<PickUpItems>().weapon.GetComponent<Weapon>().configertBullet.GetComponent<Bullet>().damage, player.GetComponent<PickUpItems>().weapon.name, GameObject.FindGameObjectWithTag("Player").GetComponent<PickUpItems>().weapon.GetComponent<Weapon>().configertBullet.name);  //set the player in Gamestate with all relavent values 
        }
        else if (bulletName == "Spark")
        {
            GameState.player = new Player(player.GetComponent<PickUpItems>().lifepoints, player.GetComponent<PickUpItems>().maxLifepoints, player.GetComponent<PickUpItems>().money, player.GetComponent<PickUpItems>().keys, GameObject.FindGameObjectWithTag("Player").GetComponent<PickUpItems>().weapon.GetComponent<Weapon>().shootingTime, GameObject.FindGameObjectWithTag("Player").GetComponent<PickUpItems>().weapon.GetComponent<Weapon>().configertBullet.GetComponent<BulletSpark>().damage, player.GetComponent<PickUpItems>().weapon.name, GameObject.FindGameObjectWithTag("Player").GetComponent<PickUpItems>().weapon.GetComponent<Weapon>().configertBullet.name); //set the player in Gamestate with all relavent values with bullet Spark 
        }
    


        if (SceneManager.GetActiveScene().name != "EndGame" && GameState.difficulty < endCondition)     //if the active sceen isn't Endgame and difficulty smaller than endCondition
        {
            GameState.nummberOfIterrations++;                       //rooms you finished ++
            if (SceneManager.GetActiveScene().name != "StartGame")
            {
                GameState.difficulty = GameObject.FindGameObjectWithTag("LevelCreater").GetComponent<LevelCreater>().spawnRate * 1.2f;
            }
            Loader.Load(Loader.Scene.Gameplay);         //open the new sceen Gameplay
        }
        else if (SceneManager.GetActiveScene().name != "EndGame" && GameState.difficulty > endCondition)    // if the active sceen isn't EndGame and the difficulty,we modifire every iteration, is bigger than the endcondition
        {
            GameState.nummberOfIterrations++;
            Loader.Load(Loader.Scene.EndGame);          //open the new sceen EndGame
        }
        else if (SceneManager.GetActiveScene().name == "EndGame")       //if active sceen is Endgame 
        {
            Loader.Load(Loader.Scene.Win);              //open the new sceen Win
        }
    }
    
}
