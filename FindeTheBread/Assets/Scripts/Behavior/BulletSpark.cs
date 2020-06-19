using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpark : MonoBehaviour {

    public AudioSource sound;
    public float damage;

    private void Awake()
    {
        foreach (Sounds s in FindObjectOfType<AudioManager>().audioClips)       //Finde the Spark sound and save the values in the audiosource
        {
            if(s.name == "ShootingSpark")
            {
                sound = gameObject.AddComponent<AudioSource>();
                sound.clip = s.audioClip;
                sound.volume = 0.209f;
                sound.Play();       //play the sound directly when its created
            }
        }
    }
   
    private void OnCollisionEnter2D(Collision2D collision)  //on collision destroy the AudioSource player and the bullet
    {
        Destroy(sound);
        Destroy(gameObject);
    }

}
