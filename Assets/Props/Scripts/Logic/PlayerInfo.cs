using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInfo : MonoBehaviour
{
    
    
    //public GameObject Field;    
    private int position;

    void Start()
    {
        position = 0;
    }

    // Update is called once per frame
    void Update()
    {
       

    }

    public void SetPosition(int Position)
    {
        position = Position;
    }

    public int GetPosition()
    {
        return position;
    }

}

