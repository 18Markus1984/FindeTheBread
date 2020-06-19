using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowAnimation : MonoBehaviour      //spike 
{
    public Animator ani;
    private float stayTime = 1.5f;  //when this value is smaller than 0 the object which stays on the spike get damage 
    private float timePuffer;       //value that we work with
    private Collider2D collider;
    private bool alowed = false;    //if true starts the timer

    private void Start()
    {
        timePuffer = stayTime;      //set puffer = staytime
    }

    private void Update()
    {
        if (collider != null && timePuffer < 0)     //if timer is smaller than 0 and their is an object with the tag enemy or player
        {
            if (SceneManager.GetActiveScene().name != "UI")
            {
                if (collider.tag == "Player")           //set the parameter false and the object gets damage
                {
                    collider.GetComponent<PickUpItems>().lifepoints -= 1;
                    ani.SetBool("somethingIsOnMe", false);
                }
                else if (collider.tag == "Enemy")
                {
                    collider.GetComponent<LifePoints>().lifePoints -= 1;
                    ani.SetBool("somethingIsOnMe", false);
                }
            }
        }
        else if (timePuffer < 0) //if timer bigger than 0 
        {
            ani.SetBool("somethingIsOnMe", false);  //set the animator parameter false
            alowed = false;
        }

        if (timePuffer > 0 && alowed)
        {
            timePuffer -= Time.deltaTime;       //reduce timer
        }
        else
        {
            timePuffer = stayTime;      //resete the puffer;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Enemy") //if enemy or player left the spike collider set the collider null that the animation don't stop
        {
            collider = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        if (collision.tag == "Player" || collision.tag == "Enemy" ) //if the object is player or enemy set  
        {
            alowed = true;
            collider = collision;
            ani.SetBool("somethingIsOnMe", true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "TielmapTop")      //if the object collides with the topTilemap set the y positon + 1f
        {
            transform.position = new Vector3(transform.position.x,transform.position.y + 1f,0);
        }
    }
}
 