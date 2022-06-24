using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMinigame : MonoBehaviour
{
    public bool minigameFinished;
    // Start is called before the first frame update
    void Start()
    {
        minigameFinished = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGame()
    {
        Debug.Log("Loading Minigame");
        minigameFinished = true;
    }
}
