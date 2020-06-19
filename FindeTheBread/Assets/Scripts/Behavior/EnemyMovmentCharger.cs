using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovmentCharger : MonoBehaviour
{
    public float followSpeed;
    public float speed;
    public float stoppingDistance;  //distance when he attacks the player
    float shakingSpeed = 50f;
    float shakingAmount = 0.03f;
    bool chargeAttack = false;  
    float chargeCounter = 0;    //counter until the enemy stopps to shake and move towards the players position
    Vector2 targetPositionSaved;
    private int destroyeTester = 700;       //timer to check if an enemy is spawned at the wrong position

    private Transform[] patrolPositions;
    private Transform puffer;
    private bool oneTime = true;
    public float stayTime;
    private float stayTimePuffer;
    private Transform positionBefore;

    private Transform target;


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

        if (Vector2.Distance(transform.position, target.position) < stoppingDistance)
        {
            if (chargeCounter <= 100 && chargeAttack)
            {
                transform.position = new Vector2(transform.position.x + Mathf.Sin(Time.time * shakingSpeed) * shakingAmount, transform.position.y);    //shaking 
                chargeCounter++;
                chargeAttack = false;
            }
            else
            {
                if (!chargeAttack)
                {
                    targetPositionSaved = target.position;
                    chargeAttack = true;                //deaktivte the charge Attack at the start
                }
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(targetPositionSaved.x - transform.position.x * 1 / 10, targetPositionSaved.y - transform.position.y * 1 / 10), followSpeed * Time.deltaTime); //move in a line towards the player
                if (Vector2.Distance(transform.position, targetPositionSaved) < 0.5f)    //reached the position
                {
                    if (stayTimePuffer < 0)     //the enemy waits at this position until the timer is smaller than 0
                    {
                        chargeCounter = 0;
                        stayTimePuffer = stayTime;  //reset the timer
                    }
                    else
                    {
                        stayTimePuffer -= Time.deltaTime;   //reduce the timer
                    }
                }
            }
        }
        else if (!oneTime)
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
            chargeCounter = 0;      //resets the counter when the player isn't in his range
        }
    }

    private void FindeNewRandomSpot()
    {
        puffer = patrolPositions[Random.Range(0, patrolPositions.Length)];      //look for a new position in the patrol point list
    }

}

