using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreBullets : MonoBehaviour
{
    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Bullet"))
        {
            Physics2D.IgnoreCollision(item.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("BulletEnemy"))
        {
            Physics2D.IgnoreCollision(item.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
