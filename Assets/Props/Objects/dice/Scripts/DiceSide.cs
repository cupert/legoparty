using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSide : MonoBehaviour
{
    bool isGrounded;
    public int sideValue;

    void OnTriggerStay(Collider col){
        if(col.tag== "Ground"){
            isGrounded=true;
        }
    }
    void OnTriggerExit(Collider col){
        if(col.tag=="Ground"){
            isGrounded=false;
        }
    }

    public bool GetOnGround(){
        return isGrounded;
    }
}
