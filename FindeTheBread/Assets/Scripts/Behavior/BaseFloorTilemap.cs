using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFloorTilemap : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)                                  //if the buttomTilemap colides
    {
        if (collision.collider.tag != "Tree" && collision.collider.tag != "Enemy")                                               //if the colider tag is "Tree" 
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider);      //ignore the collision between the the tree and the bottomTilemap
        }
    }
}
