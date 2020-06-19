using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovment : MonoBehaviour
{
    
    public float followSpeed;       
    public float speed;
    public float stoppingDistance;          //range
    private int destroyeTester = 700;       //timer to check if an enemy is spawned at the wrong position

    private Transform[] patrolPositions;
    private Transform puffer;
    private bool oneTime = true;
    public float stayTime;
    private float stayTimePuffer;
    private Transform positionBefore;

    private Transform target;   //player position
    

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        stayTimePuffer = stayTime;
    }

    private void Update()
    {
        if (oneTime)        //only onetime
        {
            patrolPositions = GameObject.FindGameObjectWithTag("LevelCreater").GetComponent<LevelCreater>().CreatePatrolPositionObjects();  //execute the function that returns positions the enemys can randomly walk to
            FindeNewRandomSpot();
            oneTime = false;
        }

        if (Vector2.Distance(transform.position, target.position) < stoppingDistance) //if the player is in range
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, followSpeed * Time.deltaTime);
        }
        else if(!oneTime)
        {            
            transform.position = Vector2.MoveTowards(transform.position, puffer.position, speed * Time.deltaTime);  //go towards the new position
            if (Vector2.Distance(transform.position,puffer.position) < 0.5f)    //reached the position
            {
                if (stayTimePuffer < 0)     //the enemy waits at this position until the timer is smaller than 0
                {
                    Debug.Log("New Position");
                    FindeNewRandomSpot();
                    stayTimePuffer = stayTime;  //reset the timer
                }
                else
                {
                    stayTimePuffer -= Time.deltaTime;   //reduce the timer
                }
            }
        }
    }

    private void FindeNewRandomSpot()
    {
        puffer = patrolPositions[Random.Range(0, patrolPositions.Length)];      //look for a new position in the patrol point list
    }

    
}
