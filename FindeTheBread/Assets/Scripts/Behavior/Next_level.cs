using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Next_level : MonoBehaviour
{
    private bool levelSolved = false;       //variable says if the level is cleared
    private int contectCounter;     //check that it take some time until the nextLevelfunction is excecuted
    private Transform player;       //player position
    private Collider2D playerCollider2D;    //player collider2D

    public void Update()
    {
        if (contectCounter > 75)    //if the player stays until the counter is 75  
        {
            player.localScale = new Vector3(player.localScale.x * (float)0.9, player.localScale.y * (float)0.9, player.localScale.z * (float)0.9);  //shrink the player
            if (player.localScale.x < 0.1 || SceneManager.GetActiveScene().name == "UI") //if the player scale is smaller than 0.1
            {
                if (SceneManager.GetActiveScene().name != "UI")
                {
                    Debug.Log("NextLevel");
                    FindObjectOfType<GameManager>().StartNextLevel();   //execute the function to start a new level
                }
                else
                {
                    Loader.Load(Loader.Scene.StartGame);
                }
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)     //if there is no GameObject with the tag enemy set level cleared
            {
                levelSolved = true;
            }
            if (levelSolved)
            {
                player = collision.collider.GetComponent<Transform>();      //set the player position to the door position
                contectCounter++;                                           //raise the counter
            }
        }
    }

    
}
