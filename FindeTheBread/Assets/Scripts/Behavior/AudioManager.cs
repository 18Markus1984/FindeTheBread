using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<Sounds> audioClips = new List<Sounds>();            //list of sound files with different parametersw

    void Awake()
    {
        foreach (Sounds s in audioClips)                            //set the data from the audioClips to the audiosource that you can play
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.audioClip;
            s.audioSource.volume = s.volume;
            s.audioSource.pitch = s.pitch;
            s.audioSource.loop = s.loop;
        }
    }

    public void Play(string soundName)                      //plays the sound with the given string
    {
        Sounds audioClipToPlay = audioClips[0];
        foreach (Sounds s in audioClips)                    //selects the sound with the same name as the string
        {
            if (s.name == soundName)                
            {
                audioClipToPlay = s;
                audioClipToPlay.audioSource.Play();         //plays the selected sound
            }
        }
    }

    public void Stop(string soundName)                      //stops the sound with the given string
    {
        Sounds audioClipToPlay = audioClips[0];
        foreach (Sounds s in audioClips)                    //selects the sound with the same name as the string
        {
            if (s.name == soundName)
            {
                audioClipToPlay = s;
                audioClipToPlay.audioSource.Stop();         //stops the selected sound
            }
        }
    }
}
