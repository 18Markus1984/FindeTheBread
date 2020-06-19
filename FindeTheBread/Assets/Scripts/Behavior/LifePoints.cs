using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePoints : MonoBehaviour     //enemy
{
    public float lifePoints;
    public float damage;

    public GameObject[] pickUpItems = new GameObject[2];


    private void Update()
    {
        if (lifePoints <= 0)        
        {
            if (Random.Range(1,5) == 1)     //dropp hearts and coins random when an enemy dies
            {
                GameObject pickUp = Instantiate(pickUpItems[Random.Range(0,pickUpItems.Length)],new Vector3(transform.position.x,transform.position.y),new Quaternion(0,0,0,0));
            }
            GameState.killedEnemies++;      //the variable killedEnemies ++ what you can see in the gamestate
            Destroy(gameObject);            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float damage = 0;
        if (collision.collider.tag == "Bullet" || collision.collider.tag == "Explosion")
        {
            if (collision.gameObject.GetComponent<Bullet>() == null)    //if collision has no Bullet 
            {
                damage = collision.gameObject.GetComponent<BulletSpark>().damage;   //set damge to the bullet damage
            }
            else
            {
                damage = collision.gameObject.GetComponent<Bullet>().damage;        //set damge to the bullet damage
            }
            lifePoints = lifePoints - damage;   //the lifepoints are decreased
        }
    }

    private bool rightPosition = false;
    private float timerPuffer = 100;


    private void OnCollisionStay2D(Collision2D collision)       //checks if all enemies re spawned right and aren't at the wrong position
    {
        if (collision.collider.tag == "TilemapBottom")          //if the enemies stand on the bottomTilemap they are spawned correctly else they will be deleted after a short periode of time that enemies that are close to the topTilemp can move away        
        {
            Debug.Log("RightPosition");
            rightPosition = true;
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
        else if (collision.collider.tag == "TilemapTop" && !rightPosition && timerPuffer < 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            timerPuffer -= 1;
        }
    }
}
