using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInfo : MonoBehaviour
{
    
    
    //public GameObject Field;    
    public int position { get; set; }
    public int coins { get; set; }
    public int trophies { get; set; }

    void Start()
    {
        position = -1;
        coins = 0;
        trophies = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

}

