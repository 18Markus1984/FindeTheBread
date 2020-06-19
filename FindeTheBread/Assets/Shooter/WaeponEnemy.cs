using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaeponEnemy : MonoBehaviour
{
    public GameObject enemy;
    private Rigidbody2D weapon;
    public Transform rotationPoint;     //point at the enemy where the weapon should rotate 
    public Transform firePoint;         //point at the weapon where the bullet is spawned
    public GameObject configertBullet;
    public float stoppingDistance;          //range
    public float shootingSpeed;             //fire rate
    private float shootingTimePuffer;       //puffer to work with the value shootingSpeed
    Transform target;                       //player position


    Vector2 mousePos;

    public float bulletForce = 20f;

    void Start()
    {
        weapon = GetComponent<Rigidbody2D>();       //Weapon will be set to the weapon rigidbody to prevent mistakes or errors
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        shootingTimePuffer = shootingSpeed;
    }

    void Update()
    {
        if(Vector2.Distance(transform.position, target.position) < stoppingDistance && shootingTimePuffer <= 0) //if the player is in range and the enemy shooting timer alows to shoot
        {
            Shoot();
        }
        else
        {
            shootingTimePuffer -= Time.deltaTime;       //shooting timer will be reduced
        }
    }

    private void FixedUpdate()
    {
        Rigidbody2D weapon = GetComponent<Rigidbody2D>();
        weapon.position = rotationPoint.transform.position;     //weapon position is set to the rotationPoint on the enemy

        Vector2 lookDirection = (Vector2)target.position - weapon.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 135;
        weapon.rotation = angle;                    //weapon always looks towards the player
    }


    private void Shoot()
    {
        GameObject bullet = Instantiate(configertBullet, firePoint.position, firePoint.rotation);       //create a new gameObject clone of bullet
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);                                   //add force that the bullet moves
        Physics2D.IgnoreCollision(enemy.GetComponent<Collider2D>(), bullet.GetComponent<Collider2D>());     //bullet collider ignore enemy collider that the bullet doesn't push the enemy away
        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GameObject.FindGameObjectWithTag("TilemapBottom").GetComponent<Collider2D>());     //ignore the bottomCollider
        shootingTimePuffer = shootingSpeed;     //reste the timer
    }
}
