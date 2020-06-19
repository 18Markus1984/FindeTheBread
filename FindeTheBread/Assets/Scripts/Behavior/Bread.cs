using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bread : MonoBehaviour
{
    public GameObject breadPosition;            //position where the bread is set to if the player collects the bread
    public bool pickedUp;                       
    public float shakingAmount;                 //the amount the bread goes down and up in the sinus function
    public float shakingSpeed;                  //the movment speed for moving with the sinus function

    // Update is called once per frame
    void Update()
    {
        if (!pickedUp)                          //if the bread isn't picked up
        {
            transform.position = new Vector2(transform.position.x , transform.position.y+ Mathf.Sin(Time.time * shakingSpeed) * shakingAmount); //the bread's shaking movemt which should like smooth flooting
        }
        if (pickedUp)                           //if the bread is picked up
        {
            transform.position = breadPosition.GetComponent<Transform>().position;      //the bread's position is set to the breadposition 
        }
    }
}
