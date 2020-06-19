using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartTutorial : MonoBehaviour
{
    public Image image;
    private float fillamaunt = 1;
    public GameObject loadingBar;              
    public GameObject soundManager;
    public Canvas speachBubble;

    [HideInInspector]
    public GameObject weapon;
    public GameObject weaponPickUpPosition;         //position whereb the random weapon will be spawned
    public GameObject text;             
    public string[] textWhatTheSignSays;                        //array with gamestate text
    private string[] audioClipNames = new string[]              //string array with the name of the audioclips
    {
        "Hello","Help","Know","Bread","Nobody","Present","PressQ","Destroy","Adventure"
    };

    public List<GameObject> weapons = new List<GameObject>();

    private bool hitBoxactive = false;          //says if the player has hide the sign collider
    private int counter = 0;                    //tells the number of the active text in the string array and the suiting sound

    public void Start()
    {
        GameState.difficulty = 1f;              //set the gamedifficulty
        GameState.killedEnemies = 0;            //reset the killed enemies when somebody want to play again
        GameState.nummberOfIterrations = 0;     //reset the survived rooms when somebody want to play again

        int random = Random.Range(0, 3);
        weapon = weapons[random];                                   //select a random weapon
        Debug.Log(weapon.name+","+weapons.Count+","+random);
        weapon.GetComponent<Transform>().transform.position = weaponPickUpPosition.GetComponent<Transform>().position;      //set the pickUpPosition to the weapon
        weapon.SetActive(false);
    }

    private void Update()
    {
        if (fillamaunt < 0)
        {
            loadingBar.SetActive(false);    //hide loadingbar
        }
        else if(counter > 0)
        {
            fillamaunt -= 0.0025f;          
            image.fillAmount = fillamaunt;  //upddate the fillamount
        }

        //skip the tutorial
        if (Input.GetKeyDown(KeyCode.E) && fillamaunt > 0 && hitBoxactive) //if the user press e and the loadingbar isn't smaller than 0 and the player hit the hitbox
        {                                                                          
            weapon.SetActive(true);                                             
            weapon.GetComponent<Weapon>().pickedUp = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PickUpItems>().weapon = weapon;
            Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>(), GetComponent<Collider2D>());
            text.GetComponent<TMPro.TextMeshProUGUI>().text = textWhatTheSignSays[8];
            soundManager.GetComponent<AudioManager>().Play(audioClipNames[8]);
            loadingBar.SetActive(false);
        }

        //show the next text and play the next sound
        if (Input.GetKeyDown(KeyCode.Space) && counter < textWhatTheSignSays.Length - 1 && hitBoxactive && !soundManager.GetComponent<AudioManager>().audioClips[counter+9-1].audioSource.isPlaying && counter < 8)
        {
            loadingBar.SetActive(false);
            fillamaunt = -1;
            text.GetComponent<TMPro.TextMeshProUGUI>().text = textWhatTheSignSays[counter];  //show the text in the string array at the position counter
            soundManager.GetComponent<AudioManager>().Play(audioClipNames[counter]);        //play the sound in audioclips with the same name 
            if (counter == 7)
            {
                weapon.SetActive(true);
            }
            counter++;
        }

        //show the last sound and text
        if (weapon.GetComponent<Weapon>().pickedUp && counter == 8)
        {
            soundManager.GetComponent<AudioManager>().Play(audioClipNames[8]);
            text.GetComponent<TMPro.TextMeshProUGUI>().text = textWhatTheSignSays[8];
            Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>(), GetComponent<Collider2D>());
            counter++;            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {       
            if (!hitBoxactive)
            {                         //if hitboxactive isn't true
                soundManager.GetComponent<AudioManager>().Play("Hello");        //plays the first sound 
                counter++;                                                      //soundCounter ++
                speachBubble.gameObject.SetActive(true);                        
            }
            hitBoxactive = true;                            //hitbox set to true because the player hit the hitbox
        }
    }
}
