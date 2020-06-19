using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBread : MonoBehaviour        //the script is in the stone under the bread
{
    public GameObject bread;                    //the bread over the stone

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")     //if stone colides with the player
        {
            bread.GetComponent<Bread>().pickedUp = true;    // set the bread variable pickedUp as true
        }
    }
}
