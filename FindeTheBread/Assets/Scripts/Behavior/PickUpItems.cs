using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class PickUpItems : MonoBehaviour    //player
{
    public float lifepoints;        
    public float maxLifepoints;
    public int money = 0;       //coins
    public int keys = 0;
    public Canvas canvas;       //Game Over canvas
    float damageGetTime = 1;
    private AudioManager managerForAudio;   //AudioPlayer

    [HideInInspector]
    public GameObject weapon;   //active weapon

    [HideInInspector]
    public GameObject newWeapon;    //new weapon you want to pick up

    [HideInInspector]
    public bool alowedToPickUp = false; //weapon is alowedToPickUp

    private void Awake()
    {
        managerForAudio = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        damageGetTime -= Time.deltaTime;    

        if (lifepoints <= 0)    //game over
        {
            GetComponent<Movement>().enabled = false;       //disable monobehavior
            canvas.gameObject.SetActive(true);              //enable game Over canvas
            GetComponent<PickUpItems>().enabled = false;    //disable monobehavior
        }

        if (Input.GetKeyDown(KeyCode.Q) && alowedToPickUp && newWeapon != null)     //pickUp Weapon
        {
            foreach (GameObject w in GameObject.FindGameObjectsWithTag("Weapon"))   //finde Weapon that is pickedUp
            {
                if (w.GetComponent<Weapon>().pickedUp)
                {
                    Destroy(w);
                    break;
                }
            }
            weapon = newWeapon;         //set active weapon to the new weapon
            weapon.GetComponent<Weapon>().pickedUp = true;
            newWeapon = null;   
            alowedToPickUp = false;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "BulletEnemy")    //hit by bullet
        {
            float damage = collision.gameObject.GetComponent<BulletEnemy>().damage;
            lifepoints = lifepoints - damage;
            managerForAudio.Play("HitByEnemyOrBullet");

            CameraShaker.Instance.ShakeOnce(damage/2,4f,.1f,1f);
        }

        if (collision.collider.gameObject.tag == "Coin")    //pickUp coin,heart,key
        {
            money++;
            Destroy(collision.collider.gameObject);
            managerForAudio.Play("PickUpCoin");
        }
        else if (collision.collider.gameObject.tag == "Key")
        {
            keys++;
            Destroy(collision.collider.gameObject);
            managerForAudio.Play("PickUpKey");
        }
        else if (collision.collider.gameObject.tag == "Heart" && lifepoints < maxLifepoints)
        {
            if (lifepoints +1 > maxLifepoints)
            {
                lifepoints = maxLifepoints;
            }
            else
            {
                lifepoints++;
            }
            Destroy(collision.collider.gameObject);
            managerForAudio.Play("PickUpHeart");
        }
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy" && damageGetTime <= 0)    //player get damgae
        {
            float damage = collision.gameObject.GetComponent<LifePoints>().damage;
            lifepoints = lifepoints - damage;
            managerForAudio.Play("HitByEnemyOrBullet");         //play AudioClip
            damageGetTime = 1;     //reset damage get time
            CameraShaker.Instance.ShakeOnce(damage/2, 4f, .1f, 1f);
        }
    }

}
