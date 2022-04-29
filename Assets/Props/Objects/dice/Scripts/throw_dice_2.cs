using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throw_dice_2 : MonoBehaviour
{

    Rigidbody rb;
    bool isLanded;
    bool isThrown;

    Vector3 initPosition;

    public int diceValue;
    public bool finished;

    public DiceSide[] diceSides;

    void Start(){
        rb= GetComponent<Rigidbody>();
        initPosition= transform.position;
        rb.useGravity=false;
    }

    void Update(){
        if(Input.GetKey("f")){
            RollDice();
        }
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
        if(!isThrown&&!isLanded){
            isThrown=true;
            rb.useGravity=true;
            rb.AddTorque(Random.Range(0,500), Random.Range(0,500), Random.Range(0,500));
        }
        else if(isThrown&&isLanded){
            Reset();
        }
    }

    void Reset(){
        transform.position=initPosition;
        isThrown=false;
        isLanded=false;
        rb.useGravity=false;
    }

    void RollAgain(){
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
