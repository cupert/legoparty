using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void LoadRandomScene(){
        Debug.Log("Loading Random Scene");
        int sceneNumber= Random.Range(1, SceneManager.sceneCountInBuildSettings);
        SceneManager.LoadScene(sceneNumber);
    }

    public void LoadMainScene(){
        Debug.Log("Loading Main Scene");
        SceneManager.LoadScene(0);
    }
}
