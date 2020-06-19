using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Weapon : MonoBehaviour {

    public GameObject player;
    private Rigidbody2D weapon;
    public Transform rotationPoint;     //point at the player where the weapon should rotate 
    public Transform firePoint;         //point at the weapon where the bullet is spawned
    public GameObject configertBullet;
    public Camera cam;
    public float rotation;              //the rotation of he differnt weaopons so that every weapon points towards the player
    public float shootingTime;          //fireRate
    private float shootingPuffer;       //puffer to work with the value shootingSpeed

    public bool pickedUp;
    Vector2 mousePos;

    public float bulletForce = 20f;

    //public GameObject[] prefabPickUps;

    // Use this for initialization
    void Start()
    {
        shootingPuffer = shootingTime;
        weapon = GetComponent<Rigidbody2D>();       //Weapon will be set to the weapon rigidbody to prevent mistakes or errors 
        if (pickedUp)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PickUpItems>().weapon = gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        shootingPuffer -= Time.deltaTime;

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetButtonDown("Fire1") && pickedUp && shootingPuffer <= 0) //left mouse click && pickedUp and the timer alowse to shoot
        {
            Shoot();
            shootingPuffer = shootingTime;      //resete the timer
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !pickedUp)
        {
            player.GetComponent<PickUpItems>().alowedToPickUp = true;       //set the variable alowedToPickUp in the player true and new weapon to this gameObject
            player.GetComponent<PickUpItems>().newWeapon = gameObject;          
        }
    }

    private void FixedUpdate()
    {
        if (pickedUp)
        {
            Rigidbody2D weapon = GetComponent<Rigidbody2D>();
            GameObject rotationPoint = GameObject.FindGameObjectWithTag("RotationPoint");
            weapon.position = rotationPoint.transform.position;   //weapon position is set to the rotation point on the player

            Vector2 lookDirection = mousePos - weapon.position;
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - rotation;
            weapon.rotation = angle;            //weapon looks towards the mouse
        }  
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(configertBullet, firePoint.position, firePoint.rotation);       //create a new gameObject clone of bullet
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        //select the right sound for the weapon
        switch (gameObject.name)       
        {
            case "NormalGun":
                FindObjectOfType<AudioManager>().Play("Shooting3");
                break;

            case "Magical Bow":
                FindObjectOfType<AudioManager>().Play("Shooting1");
                break;
        }
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);       //add movement to the bullet rigidbody
        Physics2D.IgnoreCollision(rb.GetComponent<Collider2D>(), GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>());     //bullet ignores player hitbox

        if (SceneManager.GetActiveScene().name != "StartGame" && SceneManager.GetActiveScene().name != "EndGame")
        {
            Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("TilemapBottom").GetComponent<Collider2D>(), rb.GetComponent<Collider2D>());     //bullet ignores bottomTile hitbox only in the sceen Gameplay
            //Physics2D.IgnoreCollision(prefabPickUps[0].GetComponent<Collider2D>(), rb.GetComponent<Collider2D>());
            //Physics2D.IgnoreCollision(prefabPickUps[1].GetComponent<Collider2D>(), rb.GetComponent<Collider2D>());
            //Physics2D.IgnoreCollision(prefabPickUps[2].GetComponent<Collider2D>(), rb.GetComponent<Collider2D>());
        }
    }
}
