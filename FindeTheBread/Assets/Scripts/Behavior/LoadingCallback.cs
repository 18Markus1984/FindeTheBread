using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingCallback : MonoBehaviour
{
    public bool isFirstUpdate = true;       //that we don't update more than one time

    public void Update()
    {
        if (isFirstUpdate)
        {
            isFirstUpdate = false;
            Loader.Callback();
        }
    }

}
