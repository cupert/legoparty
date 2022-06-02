using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene_Minigame : MonoBehaviour
{
    private int x=0;

    public void LoadMainScene(){
        Debug.Log("Loading Main Scene");
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(isMinigameOver()){
            Debug.Log("Minigame is over");
            LoadMainScene();
        }
    }

    bool isMinigameOver(){
        x+=1;
        if(x>= 300){
            return true;
        }
        return false;
    }
}
