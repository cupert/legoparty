using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throw_dice_2 : MonoBehaviour
{

    Rigidbody rb;
    bool isLanded;
    bool isThrown;

    Vector3 initPosition;
    Quaternion initRotation;

    public int diceValue;
    public bool finished;

    public DiceSide[] diceSides;

    void Start(){
        rb= GetComponent<Rigidbody>();
        initPosition= transform.position;
        initRotation= transform.rotation;
        rb.useGravity=false;
    }

    void Update(){
        if(rb.IsSleeping() && !isLanded && isThrown){
            isLanded=true;
            rb.useGravity=false;
            SideValueCheck();

        }
        else if(rb.IsSleeping() && isLanded && diceValue==0){
            //RollAgain();
        }
    }
    public void RollDice(){
        finished = false;
        if(isThrown&&isLanded){
            Reset();
        }
            isThrown=true;
            rb.useGravity=true;
            rb.AddTorque(Random.Range(0,500), Random.Range(0,500), Random.Range(0,500));
        
    }

    public void Reset(){
        Debug.Log(initPosition);
        transform.rotation=initRotation;
        transform.position=initPosition;
        finished = false;
        isThrown=false;
        isLanded=false;
        rb.useGravity=false;
    }

    public void RollAgain(){
        Reset();

        isThrown=true;
        rb.useGravity=true;
        rb.AddTorque(Random.Range(0,500), Random.Range(0,500), Random.Range(0,500));

    }

    void SideValueCheck(){
        diceValue=0;
        foreach(DiceSide side in diceSides){
            if(side.GetOnGround()){
                diceValue= side.sideValue;
                Debug.Log(diceValue + " has been rolled!");
            }
        }
        finished = true;
    }

}
