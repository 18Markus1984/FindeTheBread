using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    private Image image;        //image for the loading bar that we can use the fillamount

    private void Awake()
    {
        image = transform.GetComponent<Image>();    //set image to gameobject image 
    }

    private void Update()
    {
        image.fillAmount = Loader.LoadingProgress();      //set the fillamount to the progress  
    }
}
