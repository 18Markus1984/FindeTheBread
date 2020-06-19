using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float damage;            
    public GameObject hitEffect;        //animation

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);    //create a new gameObject 
        FindObjectOfType<AudioManager>().Play("Explosion");                                     //play the explosion sound    
        Destroy(effect, 5f);                //destroy the explosion after 5 secound
        Destroy(gameObject);                //destroy the bullet
    }

}
