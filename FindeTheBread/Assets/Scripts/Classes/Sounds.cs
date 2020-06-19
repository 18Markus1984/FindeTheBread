using UnityEngine;


[System.Serializable]
public class Sounds 
{
    public string name;         //soundName
    public AudioClip audioClip;     //the clip mp3 or wav
    [Range(0f,1f)]
    public float volume;
    [Range(0f, 1f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource audioSource;     //the audioSource that plays the clip     
}
