using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene_Minigame : MonoBehaviour
{
    private int x=0;
    public void LoadRandomScene(){
        Debug.Log("Loading Random Scene");
        int sceneNumber= Random.Range(1, SceneManager.sceneCountInBuildSettings);
        SceneManager.LoadScene(sceneNumber);
    }

    public void LoadMainScene(){
        Debug.Log("Loading Main Scene");
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        x+=1;
        if(x>=300){
            LoadMainScene();
        }
    }
}
