using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    Text text;
    public float theTime;
    public bool playing = false;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playing)
        {
            theTime += Time.deltaTime;
            string miliseconds = (Mathf.Floor(float.Parse(((theTime % 1).ToString()).Substring(0, 6)) * 10000)).ToString("0000");
            string sconds = (theTime % 60).ToString("00");
            string minutes = Mathf.Floor((theTime % 3600) / 60).ToString("00");
            text.text = minutes + ":" + sconds + ":" + miliseconds;
        }
    }
}
