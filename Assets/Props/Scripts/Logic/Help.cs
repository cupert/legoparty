using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;
using Object = System.Object;

public class Help : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        printHelpMessage();
    }

    // Update is called once per frame
    void Update()
    {
        // user requests help by pressing 'h'
        if (Input.GetKeyDown("h")) {
            printHelpMessage();
        }
    }

    private void printHelpMessage() {
        Debug.Log("Press 's' to start moving a player.\n " +
                "Press '1' to select Player 1, '2' for Player 2, '3' for Player 3, '4' for Player 4.\n" + 
                "Press 'r' to roll the dice after selecting a player.\n" +
                "Press 'h' for help.");
    }
}
