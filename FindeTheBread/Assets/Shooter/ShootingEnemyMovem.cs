using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemyMovem : MonoBehaviour
{
    private Transform target;       //player 
    public float speed;
    public float stoppingDistance;      //at this distance the enemy goes to the player
    public float savePosition;          //minimal distance to the player
    private int destroyeTester = 700;       //timer to check if an enemy is spawned at the wrong position

    private Transform[] patrolPositions;
    private Transform puffer;
    private bool oneTime = true;
    public float stayTime;
    private float stayTimePuffer;
    private Transform positionBefore;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();   //set the target to player position
        stayTimePuffer = stayTime;
    }

    void Update()
    {
        if (oneTime)        //only onetime
        {
            patrolPositions = GameObject.FindGameObjectWithTag("LevelCreater").GetComponent<LevelCreater>().CreatePatrolPositionObjects();  //execute the function that returns positions the enemys can randomly walk to
            FindeNewRandomSpot();
            oneTime = false;
        }

        if (Vector2.Distance(transform.position, target.position) < stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);  //go to the player
        }
        else if (Vector2.Distance(transform.position, target.position) > stoppingDistance)
        {
            if (!oneTime)
            {
                transform.position = Vector2.MoveTowards(transform.position, puffer.position, speed * Time.deltaTime);  //go towards the new position
                if (Vector2.Distance(transform.position, puffer.position) < 0.5f)    //reached the position
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
        else if (Vector2.Distance(transform.position, target.position) < savePosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, -speed * Time.deltaTime);     //go away from the player 
        }
    }

    private void FindeNewRandomSpot()
    {
        puffer = patrolPositions[Random.Range(0, patrolPositions.Length)];      //look for a new position in the patrol point list
    }

    
}
