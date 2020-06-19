using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader 
{
    private class LoadingMonoBehavior : MonoBehaviour { }

    public enum Scene {     //all Sceens
        UI,
        Gameplay,
        Loading,
        StartGame,
        EndGame,
        Win,
    }

    private static System.Action onLoaderCallback;
    private static AsyncOperation loadingAsyncOperation;

    public static void Load(Scene scene)    //load the sceen 
    {
        onLoaderCallback = () =>    //asyncOperation
        {
            GameObject loadingGameObject = new GameObject();
            loadingGameObject.AddComponent<LoadingMonoBehavior>().StartCoroutine(LoadSceneAsync(scene));    //because of the startcoroutine we can let the loading and selected scene work simultanisliy
        };
        SceneManager.LoadScene(Scene.Loading.ToString());   //open the new scene loading 
        
    }

    private static IEnumerator LoadSceneAsync(Scene scene)
    {
        yield return null;
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene.ToString());  

        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }

    public static float LoadingProgress()   
    {
        if (loadingAsyncOperation != null)
        {
            return loadingAsyncOperation.progress;      //retrun the status of the loading 
        }
        else
        {
            return 1f;
        }
    }

    public static void Callback()
    {
        if (onLoaderCallback != null) 
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }
}
